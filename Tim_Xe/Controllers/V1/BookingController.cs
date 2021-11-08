using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Service.BookingService;

namespace Tim_Xe.API.Controllers.V1
{
    [Route("api/v1/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingServiceImp;
        public BookingController(IBookingService bookingService)
        {
            _bookingServiceImp = bookingService;
        }
        [HttpGet("{id}/{status}")]
        public IEnumerable<BookingDTO> GetBookingByDriver(int id, int status)
        {
            return _bookingServiceImp.GetBookingByDriver(id, status);
        }
        [HttpPut("cancel-booking-by-customer/{code}")]
        public Task<bool> CancelBookingByCus(string code)
        {
            return _bookingServiceImp.CancelBookingByAns(code);
        }
        [HttpGet("get-booking-admin")]
        public IEnumerable<BookingDTO> GetByAdminBooking()
        {
            return _bookingServiceImp.GetAllBookingByAdmin();
        }
        [HttpGet("get-booking-group-owner/{id}")]
        public IEnumerable<BookingDTO> GetByGroupOwnerBooking(int id)
        {
            return _bookingServiceImp.GetAllBookingByManager(id);
        }
        [HttpGet("get-code-booking")]
        public async Task<string> GetCodeBooking()
        {
            return await _bookingServiceImp.FindLastBookingCode();
        }
        [HttpGet("get-booking-by-code/{code}")]
        public async Task<int> GetIdBookingByCode(string code)
        {
            return await _bookingServiceImp.FindBookingByCodeBooking(code);
        }
        [HttpPost("caculator-price")]
        public async Task<ActionResult<double>> CaculatorBooking(BookingCreatePriceDTO bookingCreatePriceDTO)
        {
            return await _bookingServiceImp.CaculatorBooking(bookingCreatePriceDTO);
        }
        [HttpPost("create-booking")]
        public async Task<ActionResult<bool>> CreateBooking(BookingCreateDTO bookingCreateDTO)
        {
            return await _bookingServiceImp.CreateBooking(bookingCreateDTO);
        }
        [HttpPut("{id}/{status}")]
        public async Task<ActionResult<bool>> UpdateBooking(int id, int status)
        {
            return await _bookingServiceImp.UpdateBooking(id, status);
        }
    }
}
