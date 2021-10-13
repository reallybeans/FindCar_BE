using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository.Entities;
using Tim_Xe.Service.BookingService;

namespace Tim_Xe.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class bookingsController: ControllerBase
    {
        private readonly BookingServiceImp _bookingServiceImp;
        public bookingsController()
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
    }
}
