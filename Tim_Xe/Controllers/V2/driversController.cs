using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Service.DriverService;

namespace Tim_Xe.API.Controllers.V2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class driversController : ControllerBase
    {
        private readonly DriverServiceImp _driverServiceImp;

        public driversController()
        {
            _driverServiceImp = new DriverServiceImp();
        }

        [HttpGet]
        public async Task<DriverOnlySearchDataDTO> GetAll()
        {
            return await _driverServiceImp.GetAllDriversAsync();
        }

        [HttpGet("{id}")]
        public async Task<DriverListDataDTO> GetAllDriversByIdManagerAsync(int id)
        {
            return await _driverServiceImp.GetAllDriversByIdManagerAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<DriverDataDTO> GetDriverById(int id)
        {
            return await _driverServiceImp.GetDriverByIdAsync(id);
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
    }
}
