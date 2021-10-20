using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Service.PriceKmService;

namespace Tim_Xe.API.Controllers.V1
{
    [Route("api/v1/price-km")]
    [ApiController]
    [EnableCors("ApiCorsPolicy")]
    public class PriceKmController : ControllerBase
    {
        private readonly PriceKmServiceImp _priceKmServiceImp;
        public PriceKmController()
        {
            _priceKmServiceImp = new PriceKmServiceImp();
        }
        [HttpGet]
        public async Task<IEnumerable<PriceKmDTO>> GetAll()
        {
            return await _priceKmServiceImp.GetAllPriceKmsAsync();
        }
        [HttpGet("{id}")]
        public async Task<PriceKmDTO> GetPriceKmById(int id)
        {
            return await _priceKmServiceImp.GetPriceKmByIdAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult<int>> CreateAsync(PriceKmCreateDTO priceKm)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data!");
            if (await _priceKmServiceImp.CreatePriceKm(priceKm) == 1)
                return Ok("Create Success!");
            else return BadRequest("Create Failed!");
        }
        [HttpPut]
        public async Task<ActionResult<int>> UpdateAsync(PriceKmUpdateDTO priceKm)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data!");
            if (await _priceKmServiceImp.UpdatePriceKm(priceKm) == 1)
                return Ok("Update Success!");
            else return BadRequest("Update Failed!");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteAsync(int id)
        {
            if (id != 0)
            {
                var resutl = await _priceKmServiceImp.DeletePriceKmAsync(id);
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
