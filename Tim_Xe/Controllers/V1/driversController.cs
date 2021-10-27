using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Service.DriverService;

namespace Tim_Xe.API.Controllers.V1
{
    [Route("api/v1/drivers")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly DriverServiceImp _driverServiceImp;

        public DriversController()
        {
            _driverServiceImp = new DriverServiceImp();
        }
        [HttpGet]
        public async Task<IEnumerable<DriverDTO>> GetAll()
        {
            return await _driverServiceImp.GetAllDriversAsync();
        }
        [HttpGet("{id}")]
        public async Task<DriverDataDTO> GetDriverById(int id)
        {
            return await _driverServiceImp.GettDriverByIdAsync(id);
        }
        [HttpPost]
        public async Task<DriverCreateDataDTO> CreateAsync(DriverCreateDTO driver)
        {
            return await _driverServiceImp.CreateDriver(driver);
        }
        [HttpPut]
        public async Task<DriverUpdateDataDTO> UpdateAsync(DriverUpdateDTO driver)
        {
            return await _driverServiceImp.UpdateDriver(driver);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteAsync(int id)
        {
            if (id != 0)
            {
                var status = await _driverServiceImp.DeleteDriverAsync(id);
                if (status)
                    return Ok("Delete Success!");
                else
                    return NotFound("Delete Failed!");
            };
            return NotFound();

        }

        [HttpPost("search")]
        public async Task<IEnumerable<DriverDTO>> GetDriverById(DriverSearchDTO driverSearchDTO)
        {
            return await _driverServiceImp.SearchDriverAsync(driverSearchDTO);
        }
    }
}
