using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Service.TransactionService
{
    public class TransactionServiceImp : ITransactionService
    {
        private readonly TimXeDBContext context;
        private readonly LocationMapping locationMapping;
        public TransactionServiceImp()
        {
            context = new TimXeDBContext();
            locationMapping = new LocationMapping();
        }
        public async Task<TransactionListDataDTO> GetAllTransaction()
        {
            List<TransactionDTO> transactionDTOs = new List<TransactionDTO>();
            try
            {
                var transactionExist = await context.Transactions.ToListAsync();
                foreach(Transaction x in transactionExist)
                {
                    TransactionDTO transactionDTO = new TransactionDTO();
                    transactionDTO.Schedule = new ScheduleDTO();
                    transactionDTO.Schedule.Address = new AddressDTO();
                    transactionDTO.Schedule.Latlng = new LatlngDTO();
                    transactionDTO.Id = x.Id;
                    transactionDTO.BookingDriverId = x.BookingDriverId;
                    transactionDTO.CustomerId = x.CustomerId;
                    transactionDTO.DriverId = x.CustomerId;
                    transactionDTO.DriverId = x.DriverId;
                    transactionDTO.Date = x.Date;
                    transactionDTO.Description = x.Description;
                    transactionDTO.Status = x.Status;
                    AddressDTO address = new AddressDTO();
                    address.Waypoint = new List<string>();
                    LatlngDTO latlng = new LatlngDTO();
                    var bookingDriver = context.BookingDrivers.Where(bd => bd.Id == x.BookingDriverId).FirstOrDefault();
                    var booking = context.Bookings.Where(bd => bd.Id == bookingDriver.IdBooking).FirstOrDefault();
                    var locations = context.Locations.Where(l => l.IdBooking == booking.Id).ToList();

                    transactionDTO.Mode = booking.Mode;
                    transactionDTO.Price = booking.PriceBooking;
                    transactionDTO.TimeWait = booking.TimeWait;
                    transactionDTO.CustomerName = booking.NameCustomer;
                    transactionDTO.StartDate = booking.StartAt;
                    //Address
                    
                    foreach(Location l in locations)
                    {
                        var origin = l.PointTypeValue == 1 ? l.Address : null;
                        var destination = l.PointTypeValue == 3 ? l.Address : null;
                        var waypoint = l.PointTypeValue == 2 ? l.Address : null;
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
                        var origins = l.PointTypeValue == 1 ? l.LatLng : null;
                        var destinations = l.PointTypeValue == 3 ? l.LatLng : null;
                        List<string> listWaypoints = new List<string>();
                        var waypoints = l.PointTypeValue == 2 ? l.LatLng : null;
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
                        //latlng.Waypoint = latlng.Waypoint.Remove(latlng.Waypoint.Length - 1);
                        transactionDTO.Schedule.Address = address;
                        transactionDTO.Schedule.Latlng = latlng;
                    }    
                    transactionDTOs.Add(transactionDTO);
                }
                return new TransactionListDataDTO("success", transactionDTOs, "success");
            }
            catch(Exception e)
            {
                return new TransactionListDataDTO("fail", null, "fail");
            }
        }

        public async Task<TransactionListDataDTO> GetTransactionByCustomer(int id)
        {
            List<TransactionDTO> transactionDTOs = new List<TransactionDTO>();
            try
            {
                var transactionExist = await context.Transactions.Where(t => t.CustomerId == id).ToListAsync();
                foreach (Transaction x in transactionExist)
                {
                    TransactionDTO transactionDTO = new TransactionDTO();
                    transactionDTO.Schedule = new ScheduleDTO();
                    transactionDTO.Schedule.Address = new AddressDTO();
                    transactionDTO.Schedule.Latlng = new LatlngDTO();
                    transactionDTO.Id = x.Id;
                    transactionDTO.BookingDriverId = x.BookingDriverId;
                    transactionDTO.CustomerId = x.CustomerId;
                    transactionDTO.DriverId = x.CustomerId;
                    transactionDTO.DriverId = x.DriverId;
                    transactionDTO.Date = x.Date;
                    transactionDTO.Description = x.Description;
                    transactionDTO.Status = x.Status;
                    AddressDTO address = new AddressDTO();
                    address.Waypoint = new List<string>();
                    LatlngDTO latlng = new LatlngDTO();
                    var bookingDriver = context.BookingDrivers.Where(bd => bd.Id == x.BookingDriverId).FirstOrDefault();
                    var booking = context.Bookings.Where(bd => bd.Id == bookingDriver.IdBooking).FirstOrDefault();
                    var locations = context.Locations.Where(l => l.IdBooking == booking.Id).ToList();

                    transactionDTO.Mode = booking.Mode;
                    transactionDTO.Price = booking.PriceBooking;
                    transactionDTO.TimeWait = booking.TimeWait;
                    transactionDTO.CustomerName = booking.NameCustomer;
                    transactionDTO.StartDate = booking.StartAt;
                    //Address

                    foreach (Location l in locations)
                    {
                        var origin = l.PointTypeValue == 1 ? l.Address : null;
                        var destination = l.PointTypeValue == 3 ? l.Address : null;
                        var waypoint = l.PointTypeValue == 2 ? l.Address : null;
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
                        var origins = l.PointTypeValue == 1 ? l.LatLng : null;
                        var destinations = l.PointTypeValue == 3 ? l.LatLng : null;
                        List<string> listWaypoints = new List<string>();
                        var waypoints = l.PointTypeValue == 2 ? l.LatLng : null;
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
                        //latlng.Waypoint = latlng.Waypoint.Remove(latlng.Waypoint.Length - 1);
                        transactionDTO.Schedule.Address = address;
                        transactionDTO.Schedule.Latlng = latlng;                       
                    }
                    transactionDTOs.Add(transactionDTO);
                }
                return new TransactionListDataDTO("success", transactionDTOs, "success");
            }
            catch (Exception e)
            {
                return new TransactionListDataDTO("fail", null, "fail");
            }
        }

        public async Task<TransactionListDataDTO> GetTransactionByDriver(int id)
        {
            List<TransactionDTO> transactionDTOs = new List<TransactionDTO>();
            try
            {
                var transactionExist = await context.Transactions.Where(t => t.DriverId == id).ToListAsync();
                foreach (Transaction x in transactionExist)
                {
                    TransactionDTO transactionDTO = new TransactionDTO();
                    transactionDTO.Schedule = new ScheduleDTO();
                    transactionDTO.Schedule.Address = new AddressDTO();
                    transactionDTO.Schedule.Latlng = new LatlngDTO();
                    transactionDTO.Id = x.Id;
                    transactionDTO.BookingDriverId = x.BookingDriverId;
                    transactionDTO.CustomerId = x.CustomerId;
                    transactionDTO.DriverId = x.CustomerId;
                    transactionDTO.DriverId = x.DriverId;
                    transactionDTO.Status = x.Status;
                    transactionDTO.Date = x.Date;
                    transactionDTO.Description = x.Description;

                    AddressDTO address = new AddressDTO();
                    address.Waypoint = new List<string>();
                    LatlngDTO latlng = new LatlngDTO();
                    var bookingDriver = context.BookingDrivers.Where(bd => bd.Id == x.BookingDriverId).FirstOrDefault();
                    var booking = context.Bookings.Where(bd => bd.Id == bookingDriver.IdBooking).FirstOrDefault();
                    var locations = context.Locations.Where(l => l.IdBooking == booking.Id).ToList();

                    transactionDTO.Mode = booking.Mode;
                    transactionDTO.Price = booking.PriceBooking;
                    transactionDTO.TimeWait = booking.TimeWait;
                    transactionDTO.CustomerName = booking.NameCustomer;
                    transactionDTO.StartDate = booking.StartAt;
                    //Address

                    foreach (Location l in locations)
                    {
                        var origin = l.PointTypeValue == 1 ? l.Address : null;
                        var destination = l.PointTypeValue == 3 ? l.Address : null;
                        var waypoint = l.PointTypeValue == 2 ? l.Address : null;
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
                        var origins = l.PointTypeValue == 1 ? l.LatLng : null;
                        var destinations = l.PointTypeValue == 3 ? l.LatLng : null;
                        List<string> listWaypoints = new List<string>();
                        var waypoints = l.PointTypeValue == 2 ? l.LatLng : null;
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
                        //latlng.Waypoint = latlng.Waypoint.Remove(latlng.Waypoint.Length - 1);
                        transactionDTO.Schedule.Address = address;
                        transactionDTO.Schedule.Latlng = latlng;                      
                    }
                    transactionDTOs.Add(transactionDTO);
                }
                return new TransactionListDataDTO("success", transactionDTOs, "success");
            }
            catch (Exception e)
            {
                return new TransactionListDataDTO("fail", null, "fail");
            }
        }
    }
}
