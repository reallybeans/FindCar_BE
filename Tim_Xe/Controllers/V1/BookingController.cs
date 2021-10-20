using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository.Models;
using Tim_Xe.Service.BookingService;

namespace Tim_Xe.API.Controllers.V1
{

    [EnableCors("ApiCorsPolicy")]
    [Route("api/v1/booking")]
    [ApiController]
    public class BookingController: ControllerBase
    {
        private readonly BookingServiceImp _bookingServiceImp;
        public BookingController()
        {
            _bookingServiceImp = new BookingServiceImp();
        }
        [HttpGet("{iddriver}/{status}")]
        public async Task<IEnumerable<BookingDTO>> GetBooking(int iddriver, int status)
        {
           return await _bookingServiceImp.GetAllBookingsAsync(iddriver, status);
        }

        [HttpPost("caculator")]
        public async Task<ActionResult<double>> CaculatorBooking(BookingCreatePriceDTO bookingCreatePriceDTO)
        {
           return await _bookingServiceImp.CaculatorBooking(bookingCreatePriceDTO);
        }
        [HttpPost("create-booking")]
        public async Task<ActionResult<bool>> CreateBooking(BookingCreateDTO bookingCreateDTO)
        {
            return await _bookingServiceImp.CreateBooking(bookingCreateDTO);
        }
    }
}
