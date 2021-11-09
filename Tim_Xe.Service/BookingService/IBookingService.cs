using System.Collections.Generic;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;

namespace Tim_Xe.Service.BookingService
{
    public interface IBookingService
    {
        IEnumerable<BookingDTO> GetBookingByDriver(int idDriver, int status);
        IEnumerable<BookingDTO> GetAllBookingByAdmin();
        IEnumerable<BookingDTO> GetAllBookingByManager(int idManager);
        IEnumerable<BookingDTO> GetListBookingByStatus(int status);
        Task<double> CaculatorBooking(BookingCreatePriceDTO bookingCreatePriceDTO);
        Task<bool> CreateBooking(BookingCreateDTO bookingCreateDTO);
        Task<bool> UpdateBooking(int idBooking, int status);
        Task<bool> CancelBookingByAns(string code);
        Task<string> FindLastBookingCode();
        Task<int> CheckStatusByCode(string code);
        Task<ResponseModel> SendNotification(NotificationModel notificationModel);
    }
}
