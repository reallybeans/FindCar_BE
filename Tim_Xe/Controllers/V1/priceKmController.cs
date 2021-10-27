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
        public async Task<PriceKmDataDTO> GetPriceKmById(int id)
        {
            return await _priceKmServiceImp.GetPriceKmByIdAsync(id);
        }
        [HttpPost]
        public async Task<PriceKmCreateDataDTO> CreateAsync(PriceKmCreateDTO priceKm)
        {
            return await _priceKmServiceImp.CreatePriceKm(priceKm);
        }
        [HttpPut]
        public async Task<PriceKmUpdateDataDTO> UpdateAsync(PriceKmUpdateDTO priceKm)
        {
            return await _priceKmServiceImp.UpdatePriceKm(priceKm);
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
