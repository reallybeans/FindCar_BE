using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository;
using Tim_Xe.Data.Repository.Entities;
using Tim_Xe.Service.Shared;

namespace Tim_Xe.Service.BookingService
{
    public class BookingServiceImp
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
                    .Where(b=> b.Status == status && b.BookingDrivers.Any(c=> c.IdDriver == iddriver)).ToListAsync();
                foreach (Booking x in bookingExisted)
                {
                    BookingDTO bookingDTO = new BookingDTO();
                    bookingDTO.Schedule = new ScheduleDTO();
                    bookingDTO.Schedule.Address = new List<AddressDTO>();
                    bookingDTO.Schedule.Latlng = new List<LatlngDTO>();
                    bookingDTO.Id = x.Id;
                    bookingDTO.NameCustomer = x.NameCustomer;
                    bookingDTO.PhoneCustomer = x.PhoneCustomer;
                    bookingDTO.StartAt = x.StartAt;
                    bookingDTO.TimeWait = x.TimeWait;
                    bookingDTO.Mode = x.Mode;
                    bookingDTO.Status = x.Status;
                    bookingDTO.Schedule.Total = x.Locations.Count;

                    
                    AddressDTO address = new AddressDTO();
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
                            address.Waypoint +=  waypoint + "|";
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
                    address.Waypoint = address.Waypoint.Remove(address.Waypoint.Length - 1);
                    latlng.Waypoint = latlng.Waypoint.Remove(latlng.Waypoint.Length - 1);
                    bookingDTO.Schedule.Address.Add(address);
                    bookingDTO.Schedule.Latlng.Add(latlng);
                    bookingDTOs.Add(bookingDTO);
                }
            } 
            catch (Exception e) {
                return null;
            }
            return bookingDTOs;
        }
        public async Task<double> CaculatorBooking(BookingCreatePriceDTO bookingCreatePriceDTO)
        {
            bookingCreatePriceDTO.City = removeUnicode.RemoveSign4VietnameseString(bookingCreatePriceDTO.City);
            bookingCreatePriceDTO.District = removeUnicode.RemoveSign4VietnameseString(bookingCreatePriceDTO.District);
                
            var groupExisted = await context.Groups.Include(g => g.IdCityNavigation)
                .Where(g => bookingCreatePriceDTO.City.ToLower().Contains(g.IdCityNavigation.CityName.ToLower())
                && bookingCreatePriceDTO.District.ToLower().Contains(g.IdCityNavigation.District.ToLower())).ToListAsync();
            int vehicleType = bookingCreatePriceDTO.VehicleType == "4 chỗ" ?  1 : 2;
            var priceKm = await context.PriceKms.Where(p => p.IdVehicleType == vehicleType).ToArrayAsync();
            if (bookingCreatePriceDTO.Mode)
            {
                var timeWait = await context.PriceTimes.Where(p => p.IdVehicleType == vehicleType).FirstOrDefaultAsync();
            }

            return 0;
        }
    }
}
