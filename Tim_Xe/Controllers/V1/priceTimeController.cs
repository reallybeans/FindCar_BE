using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Service.PriceTimeService;

namespace Tim_Xe.API.Controllers.V1
{
    [Route("api/v1/price-time")]
    [ApiController]
    public class PriceTimeController : ControllerBase
    {
        private readonly PriceTimeServiceImp _priceTimeServiceImp;
        public PriceTimeController()
        {
            _priceTimeServiceImp = new PriceTimeServiceImp();
        }
        [HttpGet]
        public async Task<IEnumerable<PriceTimeDTO>> GetAll()
        {
            return await _priceTimeServiceImp.GetAllPriceTimesAsync();
        }
        [HttpGet("{id}")]
        public async Task<PriceTimeDTO> GetPriceTimeById(int id)
        {
            return await _priceTimeServiceImp.GetPriceTimeByIdAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult<int>> CreateAsync(PriceTimeCreateDTO priceTime)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data!");
            if (await _priceTimeServiceImp.CreatePriceTime(priceTime) == 1)
                return Ok("Create Success!");
            else return BadRequest("Create Failed!");
        }
        [HttpPut]
        public async Task<ActionResult<int>> UpdateAsync(PriceTimeUpdateDTO priceTime)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data!");
            if (await _priceTimeServiceImp.UpdatePriceTime(priceTime) == 1)
                return Ok("Update Success!");
            else return BadRequest("Update Failed!");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteAsync(int id)
        {
            if (id != 0)
            {
                var resutl = await _priceTimeServiceImp.DeletePriceTimeAsync(id);
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
