using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Service.DriverService;

namespace Tim_Xe.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class driversController : ControllerBase
    {
        private readonly DriverServiceImp _driverServiceImp;

        public driversController()
        {
            _driverServiceImp = new DriverServiceImp();
        }
        [HttpGet]
        public async Task<IEnumerable<DriverDTO>> GetAll()
        {
            return await _driverServiceImp.GetAllDriversAsync();
        }
        [HttpGet("{id}")]
        public async Task<DriverDTO> GetDriverById(int id)
        {
            return await _driverServiceImp.GettDriverByIdAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult<bool>> CreateAsync(DriverCreateDTO driver)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data!");
            if (await _driverServiceImp.CreateDriver(driver))
                return Ok("Create Success!");
            else return BadRequest("Create Failed!");
        }
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateAsync(DriverUpdateDTO driver)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data!");
            if (await _driverServiceImp.UpdateDriver(driver))
                return Ok("Update Success!");
            else return BadRequest("Update Failed!");
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
