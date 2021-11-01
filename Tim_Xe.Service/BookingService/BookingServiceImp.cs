using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository;
using Tim_Xe.Data.Repository.Entities;
using Tim_Xe.Service.Shared;
using static Tim_Xe.Data.Enum.DistanceUnit;

namespace Tim_Xe.Service.BookingService
{
    public class BookingServiceImp : IBookingService
    {
        private readonly TimXeDBContext context;
        private readonly RemoveUnicode removeUnicode;
        private readonly CaculatorDistanceGG caculatorDistanceGG;
        public BookingServiceImp()
        {
            context = new TimXeDBContext();
            removeUnicode = new RemoveUnicode();
            caculatorDistanceGG = new CaculatorDistanceGG();
        }
        public async Task<IEnumerable<BookingDTO>> GetAllBookingsAsync(int id, int status)
        {
            List<BookingDTO> bookingDTOs = new List<BookingDTO>();
            try
            {
                var bookingExisted = await context.Bookings.Include(b => b.Locations)
                    .Include(b => b.BookingDrivers)
                    .Where(b => b.Status == status && b.BookingDrivers.Any(c => c.IdDriver == id)).ToListAsync();
                foreach (Booking x in bookingExisted)
                {
                    BookingDTO bookingDTO = new BookingDTO();
                    bookingDTO.Schedule = new ScheduleDTO();
                    bookingDTO.Schedule.Address = new AddressDTO();
                    bookingDTO.Schedule.Latlng = new LatlngDTO();
                    bookingDTO.Id = x.Id;
                    bookingDTO.NameCustomer = x.NameCustomer;
                    bookingDTO.PhoneCustomer = x.PhoneCustomer;
                    bookingDTO.StartAt = x.StartAt;
                    bookingDTO.TimeWait = x.TimeWait;
                    bookingDTO.Mode = x.Mode;
                    bookingDTO.Status = x.Status;
                    bookingDTO.Schedule.Total = x.Locations.Count;


                    AddressDTO address = new AddressDTO();
                    address.Waypoint = new List<string>();
                    LatlngDTO latlng = new LatlngDTO();
                    foreach (Location y in x.Locations)
                    {
                        //Address

                        var origin = y.PointTypeValue == 1 ? y.Address : null;
                        var destination = y.PointTypeValue == 3 ? y.Address : null;
                        var waypoint = y.PointTypeValue == 2 ? y.Address : null;
                        if (origin != null)
                        {
                            address.Origin = origin;
                        }
                        if (destination != null)
                        {
                            address.Destination = destination;
                        }
                        if (waypoint != null)
                        {
                            address.Waypoint.Add(waypoint);
                        }

                        //LatLng
                        var origins = y.PointTypeValue == 1 ? y.LatLng : null;
                        var destinations = y.PointTypeValue == 3 ? y.LatLng : null;
                        List<string> listWaypoints = new List<string>();
                        var waypoints = y.PointTypeValue == 2 ? y.LatLng : null;
                        if (origins != null)
                        {
                            latlng.Origin = origins;
                        }
                        if (destinations != null)
                        {
                            latlng.Destination = destinations;
                        }
                        if (waypoints != null)
                        {
                            latlng.Waypoint += waypoints + "|";
                        }
                    }
                    latlng.Waypoint = latlng.Waypoint.Remove(latlng.Waypoint.Length - 1);
                    bookingDTO.Schedule.Address = address;
                    bookingDTO.Schedule.Latlng = latlng;
                    bookingDTOs.Add(bookingDTO);
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return bookingDTOs;
        }
        public async Task<double> CaculatorBooking(BookingCreatePriceDTO bookingCreatePriceDTO)
        {
            bookingCreatePriceDTO.City = removeUnicode.RemoveSign4VietnameseString(bookingCreatePriceDTO.City);
            bookingCreatePriceDTO.VehicleType = removeUnicode.RemoveSign4VietnameseString(bookingCreatePriceDTO.VehicleType);

            var totalKm = bookingCreatePriceDTO.Km;
            var groupExisted = await context.Groups.Include(g => g.IdCityNavigation)
                .Where(g => g.IdCityNavigation.CityName.ToLower().Contains(bookingCreatePriceDTO.City.ToLower()))
                .FirstOrDefaultAsync();
            if (groupExisted == null) return 0;

            string vehicleType = bookingCreatePriceDTO.VehicleType;
            var vehicleTypes = await context.VehicleTypes.Include(v => v.PriceKms).Where(v => v.NameType.ToLower() == vehicleType.ToLower()).FirstOrDefaultAsync();
            var priceKm = await context.PriceKms.Where(v => v.IdVehicleType == vehicleTypes.Id).ToArrayAsync();
            double priceTotal = 0;
            foreach (PriceKm x in priceKm)
            {
                var pricePerKm = (groupExisted.PriceCoefficient * x.Price) / 100 + x.Price;
                if (totalKm == 0)
                {
                    break;
                }
                if (x.Km > totalKm)
                {
                    priceTotal += totalKm * pricePerKm;
                    totalKm -= totalKm;
                }
                else if (x.Km <= totalKm)
                {
                    double km = Convert.ToDouble(x.Km);
                    priceTotal += km * pricePerKm;
                    totalKm -= km;
                }
            }
            if (groupExisted != null)
                if (totalKm > 0)
                    priceTotal += totalKm * (((groupExisted.PriceCoefficient * priceKm[priceKm.Length - 1].Price) / 100 + priceKm[priceKm.Length - 1].Price) - 1000);

            if (bookingCreatePriceDTO.Mode == true)
            {
                var timeWait = await context.PriceTimes.Where(p => p.IdVehicleType == vehicleTypes.Id).FirstOrDefaultAsync();
                double time = Convert.ToDouble(bookingCreatePriceDTO.TimeWait);
                priceTotal += (double)(timeWait.Price * (time * 24));
            }
            return priceTotal;
        }
        public async Task<bool> CreateBooking(BookingCreateDTO bookingCreateDTO)
        {
            //Find group Existed 
            bookingCreateDTO.City = removeUnicode.RemoveSign4VietnameseString(bookingCreateDTO.City);

            var groupExisted = await context.Groups.Include(g => g.IdCityNavigation)
                .Where(g => g.IdCityNavigation.CityName == bookingCreateDTO.City)
                .FirstOrDefaultAsync();

            if (groupExisted == null) return false;

            string vehicleType = bookingCreateDTO.VehicleType;
            var vehicleTypes = await context.VehicleTypes.Where(v => v.NameType == vehicleType).FirstOrDefaultAsync();

            if (bookingCreateDTO.Mode == false)
                bookingCreateDTO.TimeWait = 0;

            try
            {
                int count = 0;
                Booking booking = new Booking();
                booking.IdGroup = groupExisted.Id;
                booking.IdCustomer = bookingCreateDTO.IdCustomer;
                booking.NameCustomer = bookingCreateDTO.NameCustomer;
                booking.PhoneCustomer = bookingCreateDTO.PhoneCustomer;
                booking.IdVehicleType = vehicleTypes.Id;
                booking.StartAt = bookingCreateDTO.StartAt;
                booking.EndAt = bookingCreateDTO.StartAt.AddHours(bookingCreateDTO.TimeWait + bookingCreateDTO.TotalTime);
                booking.TimeWait = bookingCreateDTO.TimeWait;
                booking.PriceBooking = bookingCreateDTO.PriceBooking;
                booking.CreateAt = DateTime.Now;
                booking.Status = 1;
                booking.Mode = bookingCreateDTO.Mode;
                List<Location> location = new List<Location>();
                location.Add(new Location()
                {
                    LatLng = Regex.Replace(bookingCreateDTO.Schedule.Latlng.Origin, @"\s+", ""),
                    Address = bookingCreateDTO.Schedule.Address.Origin,
                    PointTypeValue = 1,
                    OrderNumber = ++count,
                });
                var wayadd = bookingCreateDTO.Schedule.Address.Waypoint;
                var wayll = bookingCreateDTO.Schedule.Latlng.ListWaypoint;
                //foreach (string wayPoints in bookingCreateDTO.Schedule.Address.Waypoint)
                if (bookingCreateDTO.Schedule.Address.Waypoint != null)
                    for (var i = 0; i < bookingCreateDTO.Schedule.Address.Waypoint.Count; i++)
                        location.Add(new Location()
                        {
                            LatLng = Regex.Replace(wayll[i], @"\s+", ""),
                            Address = wayadd[i],
                            PointTypeValue = 2,
                            IdBooking = booking.Id,
                            OrderNumber = ++count,
                        });
                location.Add(new Location()
                {
                    LatLng = Regex.Replace(bookingCreateDTO.Schedule.Latlng.Destination, @"\s+", ""),
                    Address = bookingCreateDTO.Schedule.Address.Destination,
                    PointTypeValue = 3,
                    IdBooking = booking.Id,
                    OrderNumber = ++count,
                });
                foreach (Location x in location)
                    booking.Locations.Add(x);

                var list = findListDriveActive(groupExisted.Id, booking.StartAt, booking.EndAt);
                int adaptDriver = findDriver(bookingCreateDTO.Schedule.Latlng.Origin, 0, vehicleTypes.Id, list);
                if (adaptDriver == 0) return false;

                booking.BookingDrivers.Add(new BookingDriver()
                {
                    IdDriver = adaptDriver,
                    Status = "Đang xử lý",
                    Note = bookingCreateDTO.Note,
                    Notificatied = false,
                });
                context.Bookings.Add(booking);
                context.SaveChanges();
            }
            catch (Exception e)
            { return false; }
            return true;
        }
        public double calculateTheFit(double distance, int? review, double? revenue)
        {
            double x = (double)(5 * distance + 3 * revenue - 2 * Convert.ToDouble(review));
            return x;
        }

        public async Task<bool> UpdateBooking(int idBooking, int status)
        {
            try
            {
                var bookingExisted = await context.Bookings.Include(b => b.Locations).FirstOrDefaultAsync(b => b.Id == idBooking);
                if (bookingExisted == null)
                    return false;

                var bookingDriverExisted = await context.BookingDrivers.FirstOrDefaultAsync(b => b.IdBooking == bookingExisted.Id);
                string statusStr = "";
                string latlngOrign = "";
                var list = findListDriveActive(bookingExisted.IdGroup, bookingExisted.StartAt, bookingExisted.EndAt);
                foreach (Location x in bookingExisted.Locations)
                {
                    if (x.PointTypeValue == 1)
                    {
                        latlngOrign = x.LatLng;
                        break;
                    }
                }
                switch (status)
                {
                    case 1:
                        statusStr = "Đang xử lý";
                        bookingExisted.Status = status;
                        bookingDriverExisted.Status = statusStr;
                        break;
                    case 2:
                        statusStr = "Đã Nhận";
                        bookingExisted.Status = status;
                        bookingDriverExisted.Status = statusStr;
                        break;

                    case 3:
                        statusStr = "Đã chạy";
                        bookingExisted.Status = status;
                        bookingDriverExisted.Status = statusStr;
                        context.Transactions.Add(new Transaction()
                        {
                            BookingDriverId = bookingDriverExisted.Id,
                            CustomerId = bookingExisted.IdCustomer,
                            DriverId = bookingDriverExisted.IdDriver,
                            Status = "Done",
                            Date = DateTime.Now,
                            Description = "Done"
                        });
                        break;
                    case 4:
                        statusStr = "Đang xử lý";
                        bookingExisted.Status = 1;
                        bookingDriverExisted.Status = statusStr;
                        context.Transactions.Add(new Transaction()
                        {
                            BookingDriverId = bookingDriverExisted.Id,
                            CustomerId = bookingExisted.IdCustomer,
                            DriverId = bookingDriverExisted.IdDriver,
                            Status = "Cancel",
                            Date = DateTime.Now,
                            Description = "Cancel"
                        });
                        await context.SaveChangesAsync();

                        bookingDriverExisted.IdDriver = findDriver(latlngOrign, bookingDriverExisted.Id, bookingExisted.IdVehicleType, list);
                        if (bookingDriverExisted.IdDriver == 0) return false;
                        break;
                    default:
                        return false;
                }

                context.Update(bookingExisted);
                context.Update(bookingDriverExisted);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
        public List<Driver> findListDriveActive(int? id, DateTime? start1, DateTime? end1)
        {
            var listDrivers = context.Drivers.Include(d => d.Vehicles).Where(d => d.GroupId == id).ToList();
            var listbooking = context.Bookings.Include(b => b.BookingDrivers).Where(d => ((start1 >= d.StartAt && start1 <= d.EndAt) ||
    (end1 >= d.StartAt && end1 <= d.EndAt)) && d.Status == 2).ToList();
            var list = new List<Driver>();
            if (listbooking.Count != 0)
            {
                foreach (Driver x in listDrivers)
                    foreach (Booking y in listbooking)
                        foreach (BookingDriver z in y.BookingDrivers)
                            if (z.IdDriver != x.Id)
                                list.Add(x);
            }
            else foreach (Driver x in listDrivers)
                    list.Add(x);


            return list;
        }
        public int findDriver(string origin, int idBookingDriver, int? IdVehicleType, List<Driver> list)
        {
            //get list driver 
            //var listDrivers = context.Drivers.Include(d => d.Vehicles).Where(d => d.GroupId == id).ToList();
            var listDrivers = list;


            var listDriversCancel = context.Transactions
                .Where(d => d.BookingDriverId == idBookingDriver)
                .Select(d => d.DriverId).ToArray();

            double d1 = 0, d2 = 0;
            int c1 = 0, c2 = 0;
            bool check = false;

            foreach (Driver x in listDrivers)
            {
                check = false;

                foreach (Vehicle y1 in x.Vehicles)
                {
                    if (y1.IdVehicleType != IdVehicleType)
                    {
                        check = true;
                        continue;
                    }
                }
                foreach (int y2 in listDriversCancel)
                {
                    if (y2 == x.Id)
                    {
                        check = true;
                        continue;
                    }

                }
                if (check)
                    continue;
                var distance = caculatorDistanceGG.HaversineDistance(x.Latlng, origin, DistanceUnits.Kilometers);
                var driver = context.Drivers.Where(d => d.Id == x.Id).SingleOrDefault();
                var review = (driver.ReviewScore != null ? driver.ReviewScore : 0);
                var revenue =(driver.Revenue != null ? driver.Revenue : 0);
                var rs = calculateTheFit(distance, review, revenue);
                c2 = x.Id;
                if (d1.Equals(0) && d2.Equals(0))
                {
                    c1 = x.Id;
                    d1 = rs;
                    continue;
                }
                else d2 = rs;
                if (d1 < d2)
                {
                    d1 = d2;
                    c1 = c2;
                }
            }
            return c1;
        }
    }
}
