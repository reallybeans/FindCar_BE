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

namespace Tim_Xe.Service.BookingService
{
    public class BookingServiceImp : IBookingService
    {
        private readonly TimXeDBContext context;
        private readonly RemoveUnicode removeUnicode;
        public BookingServiceImp()
        {
            context = new TimXeDBContext();
            removeUnicode = new RemoveUnicode();
        }
        public async Task<IEnumerable<BookingDTO>> GetAllBookingsAsync(int iddriver, int status)
        {
            List<BookingDTO> bookingDTOs = new List<BookingDTO>();
            try
            {
                var bookingExisted = await context.Bookings.Include(b => b.Locations)
                    .Include(b => b.BookingDrivers)
                    .Where(b => b.Status == status && b.BookingDrivers.Any(c => c.IdDriver == iddriver)).ToListAsync();
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
            bookingCreatePriceDTO.District = removeUnicode.RemoveSign4VietnameseString(bookingCreatePriceDTO.District);
            var totalKm = bookingCreatePriceDTO.Km;
            var groupExisted = await context.Groups.Include(g => g.IdCityNavigation)
                .Where(g => g.IdCityNavigation.CityName.Contains(bookingCreatePriceDTO.City)
                && g.Address.ToLower().Contains(bookingCreatePriceDTO.District.ToLower()))
                .FirstOrDefaultAsync();
            // random Group to have booking
            if (groupExisted == null)
            {
                var listGroupExisted = await context.Groups.Include(g => g.IdCityNavigation)
.Where(g => g.IdCityNavigation.CityName == bookingCreatePriceDTO.City)
.ToListAsync();
                //check lỗi ở đây
                if (listGroupExisted == null) return 0;
                var random = new Random();
                int index = random.Next(listGroupExisted.Count);
                groupExisted = listGroupExisted[index];
            }

            int vehicleType = bookingCreatePriceDTO.VehicleType == "4 chỗ" ? 1 : 2;
            var priceKm = await context.PriceKms.Where(p => p.IdVehicleType == vehicleType).ToArrayAsync();
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
                var timeWait = await context.PriceTimes.Where(p => p.IdVehicleType == vehicleType).FirstOrDefaultAsync();
                double time = Convert.ToDouble(bookingCreatePriceDTO.TimeWait);
                priceTotal += (double)(timeWait.Price * (time * 24));
            }
            return priceTotal;
        }
        public async Task<bool> CreateBooking(BookingCreateDTO bookingCreateTO)
        {
            //Find group Existed 
            bookingCreateTO.City = removeUnicode.RemoveSign4VietnameseString(bookingCreateTO.City);
            bookingCreateTO.District = removeUnicode.RemoveSign4VietnameseString(bookingCreateTO.District);

            var groupExisted = await context.Groups.Include(g => g.IdCityNavigation)
                .Where(g => g.IdCityNavigation.CityName == bookingCreateTO.City
                && g.Address.ToLower().Contains(bookingCreateTO.District.ToLower()))
                .FirstOrDefaultAsync();
            // random Group to have booking
            if (groupExisted == null)
            {
                var listGroupExisted = await context.Groups.Include(g => g.IdCityNavigation)
                                       .Where(g => g.IdCityNavigation.CityName == bookingCreateTO.City)
                                       .ToListAsync();
                //check lỗi ở đây
                if (listGroupExisted == null) return false;
                var random = new Random();
                int index = random.Next(listGroupExisted.Count);
                groupExisted = listGroupExisted[index];
            }
            return true;
        }
    }
}
