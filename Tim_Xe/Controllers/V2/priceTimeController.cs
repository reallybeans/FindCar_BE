using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Service.PriceTimeService;

namespace Tim_Xe.API.Controllers.V2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class priceTimeController : ControllerBase
    {
        private readonly PriceTimeServiceImp _priceTimeServiceImp;
        public priceTimeController()
        {
            _priceTimeServiceImp = new PriceTimeServiceImp();
        }
        [HttpGet]
        public async Task<PriceTimeListDataDTO> GetAll()
        {
            return await _priceTimeServiceImp.GetAllPriceTimesAsync();
        }
        [HttpGet("{id}")]
        public async Task<PriceTimeDataDTO> GetPriceTimeById(int id)
        {
            return await _priceTimeServiceImp.GetPriceTimeByIdAsync(id);
        }
        [HttpPost]
        public async Task<PriceTimeCreateDataDTO> CreateAsync(PriceTimeCreateDTO priceTime)
        {
            return await _priceTimeServiceImp.CreatePriceTime(priceTime);
        }
        [HttpPut]
        public async Task<PriceTimeUpdateDataDTO> UpdateAsync(PriceTimeUpdateDTO priceTime)
        {
            return await _priceTimeServiceImp.UpdatePriceTime(priceTime);
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
