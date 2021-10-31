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
        private readonly BookingServiceImp _bookingServiceImp;
        public BookingController()
        {
            _bookingServiceImp = new BookingServiceImp();
        }
        [HttpGet("{id}/{status}")]
        public async Task<IEnumerable<BookingDTO>> GetBooking(int id, int status)
        {
            return await _bookingServiceImp.GetAllBookingsAsync(id, status);
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
