using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository;
using Tim_Xe.Data.Repository.Entities;
using Tim_Xe.Service.Shared;
using static Tim_Xe.Data.Enum.DistanceUnit;
using Tim_Xe.Service.NotificationService.Google;
using static Tim_Xe.Data.Models.GoogleNotification;

namespace Tim_Xe.Service.BookingService
{
    public class BookingServiceImp : IBookingService
    {
        private readonly TimXeDBContext context;
        private readonly RemoveUnicode removeUnicode;
        private readonly CaculatorDistanceGG caculatorDistanceGG;
        private readonly FcmNotificationSetting _fcmNotificationSetting;
        private readonly string MESS = "Ban có chuyến xe mới. Nhận khách ngay nào!";
        public BookingServiceImp(IOptions<FcmNotificationSetting> settings)
        {
            _fcmNotificationSetting = settings.Value;
            context = new TimXeDBContext();
            removeUnicode = new RemoveUnicode();
            caculatorDistanceGG = new CaculatorDistanceGG();
        }
        public IEnumerable<BookingDTO> GetBookingByDriver(int idDriver, int status)
        {
            List<BookingDTO> bookingDTOs = new List<BookingDTO>();
            bookingDTOs = GetListBookingAsync(idDriver, status);
            return bookingDTOs;
        }
        public IEnumerable<BookingDTO> GetAllBookingByAdmin()
        {
            List<BookingDTO> bookingDTOs = new List<BookingDTO>();
            bookingDTOs = GetListBookingAsync(0, 0);
            return bookingDTOs;
        }
        public IEnumerable<BookingDTO> GetAllBookingByManager(int idManager)
        {
            List<BookingDTO> bookingDTOs = new List<BookingDTO>();
            bookingDTOs = GetListBookingAsync(idManager, 0);
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
                priceTotal *= 1.9;
                var timeWait = await context.PriceTimes.Where(p => p.IdVehicleType == vehicleTypes.Id).FirstOrDefaultAsync();
                double time = Convert.ToDouble(bookingCreatePriceDTO.TimeWait);
                priceTotal += (double)(timeWait.Price * (time / 60));
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
            else bookingCreateDTO.TotalTime *= 2;
            try
            {
                int count = 0;
                Booking booking = new Booking();
                booking.IdGroup = groupExisted.Id;
                booking.Code = bookingCreateDTO.Code;
                booking.IdCustomer = bookingCreateDTO.IdCustomer;
                booking.NameCustomer = bookingCreateDTO.NameCustomer;
                booking.PhoneCustomer = bookingCreateDTO.PhoneCustomer;
                booking.IdVehicleType = vehicleTypes.Id;
                booking.StartAt = bookingCreateDTO.StartAt;
                booking.EndAt = bookingCreateDTO.StartAt.AddHours(bookingCreateDTO.TotalTime + (bookingCreateDTO.TimeWait / 60));
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
                if (list.Count == 0) return false;
                int adaptDriver = await findDriverAsync(bookingCreateDTO.Schedule.Latlng.Origin, 0, vehicleTypes.Id, list, MESS);
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
                        if(list.Count == 0)
                        {
                            statusStr = "Hủy";
                            bookingExisted.Status = 4;
                            bookingDriverExisted.Status = statusStr;
                            break;
                        }
                        bookingDriverExisted.IdDriver = await findDriverAsync(latlngOrign, bookingDriverExisted.Id, bookingExisted.IdVehicleType, list, MESS);
                        if (bookingDriverExisted.IdDriver == 0) return false;
                        break;
                    case 5:
                        statusStr = "Hủy";
                        bookingExisted.Status = 4;
                        bookingDriverExisted.Status = statusStr;
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
    (end1 >= d.StartAt && end1 <= d.EndAt)) && d.Status == 2 || d.Status == 1).ToList();
            var list = new List<Driver>();
            if (listbooking.Count != 0)
            {
                foreach (Driver z in listDrivers)
                {
                    var check = false;
                    foreach (Booking x in listbooking)
                        foreach (BookingDriver y in x.BookingDrivers)
                            if (y.IdDriver == z.Id)
                            {
                                check = true;
                            }
                           
                    if (check) continue;
                    else
                    {
                        if(list.Count == 0)
                        {
                            list.Add(z);
                            continue;
                        }
                        foreach (Driver k in list)
                            if (k.Id == z.Id)
                            {
                                continue;
                            }
                            else
                            {
                                list.Add(z);
                                break;
                            }
                    }
                }



            }
            else foreach (Driver x in listDrivers)
                    list.Add(x);


            return list;
        }
        public async Task<int> findDriverAsync(string origin, int idBookingDriver, int? IdVehicleType, List<Driver> list, string mess)
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
                foreach (Vehicle y1 in x.Vehicles)
                {
                    if (y1.IdVehicleType != IdVehicleType || y1.IsDelete == true || y1.Status.Equals("unuse"))
                        continue;
                    var distance = caculatorDistanceGG.HaversineDistance(x.Latlng, origin, DistanceUnits.Kilometers);
                    var driver = context.Drivers.Where(d => d.Id == x.Id).SingleOrDefault();
                    var review = (driver.ReviewScore != null ? driver.ReviewScore : 0);
                    var revenue = (driver.Revenue != null ? driver.Revenue : 0);
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
            }
            if (c1 != 0)
            {
                NotificationModel notificationModel = new NotificationModel();
                //hardcode mode Androiod
                notificationModel.IsAndroiodDevice = true;
                notificationModel.Body = mess;
                foreach (Driver x in listDrivers)
                {
                    if (c1 == x.Id)
                    {
                        notificationModel.DeviceId = x.DiviceId;
                        var rs = await SendNotification(notificationModel);
                        break;
                    }
                }
            }
            return c1;
        }

        public async Task<string> FindLastBookingCode()
        {
            try
            {
                var bookingExisted = await context.Bookings.OrderByDescending(x => x.Id)
         .FirstOrDefaultAsync();
                return bookingExisted.Code;
            }
            catch (Exception e)
            {
                return "0";
            }
        }

        public async Task<int> FindBookingByCodeBooking(string code)
        {
            try
            {
                var bookingExisted = await context.Bookings.FirstOrDefaultAsync(b => b.Code == code);
                return bookingExisted == null ? 0 : bookingExisted.Id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public async Task<bool> CancelBookingByAns(string code)
        {

            try
            {
                var bookingExisted = await context.Bookings.Include(b => b.BookingDrivers).FirstOrDefaultAsync(b => b.Code == code);
                if (bookingExisted == null) return false;
                bookingExisted.Status = 4;
                bookingExisted.BookingDrivers.ElementAt(0).Status = "Hủy";
                context.Update(bookingExisted);
                context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            throw new NotImplementedException();
        }

        public List<BookingDTO> GetListBookingAsync(int id, int status)
        {
            List<BookingDTO> list = new List<BookingDTO>();
            try
            {
                var bookingExisted = new List<Booking>();
                if (id != 0 && status != 0)
                {
                    bookingExisted = context.Bookings.OrderBy(b => b.CreateAt).Include(b => b.Locations)
                           .Include(b => b.BookingDrivers)
                           .Where(b => b.Status == status && b.BookingDrivers.Any(c => c.IdDriver == id)).ToList();
                }
                else if (id != 0 && status == 0)
                {
                    var managerExisted = context.Managers.Include(m => m.Groups).FirstOrDefault(m => m.Id == id);
                    bookingExisted = context.Bookings.OrderBy(b => b.CreateAt).Include(b => b.Locations)
                    .Include(b => b.BookingDrivers)
                    .Where(b => b.IdGroup == managerExisted.Groups.ElementAt(0).Id).ToList();
                }
                else
                {
                    bookingExisted = context.Bookings.OrderBy(b => b.CreateAt).Include(b => b.Locations)
                    .Include(b => b.BookingDrivers)
                    .ToList();
                }
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
                    bookingDTO.PriceBooking = (double)x.PriceBooking;


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
                    list.Add(bookingDTO);
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return list;
        }

        public async Task<ResponseModel> SendNotification(NotificationModel notificationModel)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                if (notificationModel.IsAndroiodDevice)
                {
                    /* FCM Sender (Android Device) */
                    FcmSettings settings = new FcmSettings()
                    {
                        SenderId = _fcmNotificationSetting.SenderId,
                        ServerKey = _fcmNotificationSetting.ServerKey
                    };
                    HttpClient httpClient = new HttpClient();

                    string authorizationKey = string.Format("keyy={0}", settings.ServerKey);
                    string deviceToken = notificationModel.DeviceId;

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                    httpClient.DefaultRequestHeaders.Accept
                            .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    DataPayload dataPayload = new DataPayload();
                    dataPayload.Title = "Bạn có thông báo mới";
                    dataPayload.Body = notificationModel.Body;

                    GoogleNotification notification = new GoogleNotification();
                    notification.Data = dataPayload;
                    notification.Notification = dataPayload;

                    var fcm = new FcmSender(settings, httpClient);
                    var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);

                    if (fcmSendResponse.IsSuccess())
                    {
                        response.IsSuccess = true;
                        response.Message = "Notification sent successfully";
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = fcmSendResponse.Results[0].Error;
                        return response;
                    }
                }
                else
                {
                    /* Code here for APN Sender (iOS Device) */
                    //var apn = new ApnSender(apnSettings, httpClient);
                    //await apn.SendAsync(notification, deviceToken);
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
                return response;
            }
        }
    }
}
