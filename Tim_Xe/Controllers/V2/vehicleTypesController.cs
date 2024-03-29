﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Service.VehicleTypeService;

namespace Tim_Xe.API.Controllers.V2
{
    [Authorize(Roles = "group, admin, driver")]
    [Route("api/v2/vehicle-types")]
    [ApiController]
    public class VehicleTypesController : ControllerBase
    {
        private readonly VehicleTypeServiceImp _vehicleTypeServiceImp;
        public VehicleTypesController()
        {
            _vehicleTypeServiceImp = new VehicleTypeServiceImp();
        }
        [HttpGet]
        public async Task<VehicleTypeListDataDTO> GetAll()
        {
            return await _vehicleTypeServiceImp.GetAllVehicleTypesAsync();
        }
        [HttpGet("{id}")]
        public async Task<VehicleTypeDTO> GetVehicleTypeById(int id)
        {
            return await _vehicleTypeServiceImp.GetVehicleTypeByIdAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult<int>> CreateAsync(VehicleTypeCreateDTO vehicleType)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data!");
            if (await _vehicleTypeServiceImp.CreateVehicleType(vehicleType) == 1)
                return Ok("Create Success!");
            else return BadRequest("Create Failed!");
        }
        [HttpPut]
        public async Task<ActionResult<int>> UpdateAsync(VehicleTypeUpdateDTO vehicleType)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data!");
            if (await _vehicleTypeServiceImp.UpdateVehicleType(vehicleType) == 1)
                return Ok("Update Success!");
            else return BadRequest("Update Failed!");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteAsync(int id)
        {
            if (id != 0)
            {
                var resutl = await _vehicleTypeServiceImp.DeleteVehicleTypeAsync(id);
                if (resutl)
                {
                    return Ok("Delete Success!");
                }
                else return BadRequest("Delete Failed!");
            };
            return NotFound();
        }
    }
}
