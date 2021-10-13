using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tim_Xe.Service.BookingService;

namespace Tim_Xe.API.Controllers.V2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class bookingController: ControllerBase
    {
        private readonly BookingServiceImp _bookingServiceImp;
        public bookingController()
        {
            _bookingServiceImp = new BookingServiceImp();
        }
    }
}
