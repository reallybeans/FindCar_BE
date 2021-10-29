using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
                .Where(g => g.IdCityNavigation.CityName.Contains(bookingCreatePriceDTO.City))
                .FirstOrDefaultAsync();
            if (groupExisted == null) return 0;
            //            random Group to have booking
            //            if (groupExisted == null)
            //            {
            //                var listGroupExisted = await context.Groups.Include(g => g.IdCityNavigation)
            //.Where(g => g.IdCityNavigation.CityName == bookingCreatePriceDTO.City)
            //.ToListAsync();
            //                check lỗi ở đây
            //                if (listGroupExisted == null) return 0;
            //                var random = new Random();
            //                int index = random.Next(listGroupExisted.Count);
            //                groupExisted = listGroupExisted[index];
            //            }

            string vehicleType = bookingCreatePriceDTO.VehicleType;
            var vehicleTypes = await context.VehicleTypes.Include(v => v.PriceKms).Where(v => v.NameType == vehicleType).FirstOrDefaultAsync();
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
                booking.StartAt = DateTime.Now;
                booking.TimeWait = bookingCreateDTO.TimeWait;
                booking.PriceBooking = bookingCreateDTO.PriceBooking;
                booking.CreateAt = DateTime.Now;
                booking.Status = 0;
                booking.Mode = true ;
                List<Location> location = new List<Location>();
                location.Add(new Location()
                {
                    LatLng = bookingCreateDTO.Schedule.Latlng.Origin,
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
                            LatLng = wayll[i],
                            Address = wayadd[i],
                            PointTypeValue = 2,
                            IdBooking = booking.Id,
                            OrderNumber = ++count,
                        });
                location.Add(new Location()
                {
                    LatLng = bookingCreateDTO.Schedule.Latlng.Destination,
                    Address = bookingCreateDTO.Schedule.Address.Destination,
                    PointTypeValue = 3,
                    IdBooking = booking.Id,
                    OrderNumber = ++count,
                });
                foreach(Location x in location)
                    booking.Locations.Add(x);

                //get list driver 
                var listDrivers = await context.Drivers.Include(d => d.Vehicles).Where(d => d.GroupId == groupExisted.Id).ToListAsync();
                //double[] min = new double[listDrivers.Count];
                //int count1 = 0;
                //min[count1++] = distance;
                //orther method list => toArray
                double d1 = 0, d2 = 0;
                int c1 = 0, c2 = 0;
                foreach (Driver x in listDrivers)
                {
                  var distance = caculatorDistanceGG.HaversineDistance(x.Address, bookingCreateDTO.Schedule.Latlng.Origin, DistanceUnits.Kilometers);
                    c2++;
                    if (d1.Equals(0) && d2.Equals(0))
                    {
                        c1 = 0;
                        d1 = distance;
                        continue;
                    } else d2 = distance;
                    if (d1 < d2)
                    {
                        d1 = d2;
                        c1 = c2;
                    }
                }
               
            } 
            catch (Exception e) 
            { return false; }
            return true;
        }
    }
}
