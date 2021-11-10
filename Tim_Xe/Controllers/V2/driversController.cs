using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Service.DriverService;

namespace Tim_Xe.API.Controllers.V2
{

    [Authorize(Roles = "group, admin, driver")]
    [Route("api/v2/drivers")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly DriverServiceImp _driverServiceImp;

        public DriversController()
        {
            _driverServiceImp = new DriverServiceImp();
        }
        [HttpGet]
        public async Task<DriverOnlySearchDataDTO> GetAll()
        {
            return await _driverServiceImp.GetAllDriversAsync();
        }
        [HttpGet("{id}")]
        public async Task<DriverOnlyDataDTO> GetDriverByIdAsync(int id)
        {
            return await _driverServiceImp.GetDriverByIdAsync(id);
        }
        [HttpGet("drivers-in-group/{id}")]
        public async Task<DriverListDataDTO> GetAllDriversByIdManagerAsync(int id)
        {
            return await _driverServiceImp.GetAllDriversByIdManagerAsync(id);
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
        [HttpPut("update-address")]
        public async Task<DriverUpdateAddressDataDTO> UpdateAddressByIdAsync(DriverUpdateAddressDTO driver)
        {
            return await _driverServiceImp.UpdateAddress(driver);
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
        public async Task<DriverSearchDataDTO> GetDriverById(DriverSearchDTO driverSearchDTO)
        {
            return await _driverServiceImp.SearchDriverAsync(driverSearchDTO);
        }
        [HttpPut("update-status")]
        public async Task<DriverUpdateStatusDataDTO> UpdateStatusByIdAsync(DriverUpdateStatusDTO driver)
        {
            return await _driverServiceImp.UpdateStatus(driver);
        }
        [HttpPost("search-by-name")]
        public async Task<DriverOnlySearchDataDTO> SearchByName(string name)
        {
            return await _driverServiceImp.SearchByName(name);
        }
        [HttpPost("search-by-phone")]
        public async Task<DriverOnlySearchDataDTO> SearchByPhone(string phone)
        {
            return await _driverServiceImp.SearchByPhone(phone);
        }
        [HttpPost("searchs")]
        public async Task<DriverOnlySearchDataDTO> SearchDrivers(string search)
        {
            return await _driverServiceImp.SearchDrivers(search);
        }
    }
}
