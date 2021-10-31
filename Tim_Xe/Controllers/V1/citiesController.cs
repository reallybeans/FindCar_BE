using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Service.CityService;

namespace Tim_Xe.API.Controllers.V1
{
    [Route("api/v1/cities")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly CityServiceImp _cityServiceImp;

        public CitiesController()
        {
            _cityServiceImp = new CityServiceImp();
        }
        [HttpGet]
        public async Task<CityListDataDTO> GetAll()
        {
            return await _cityServiceImp.GetAllCitiesAsync();
        }
        [HttpGet("{id}")]
        public async Task<CityDataDTO> GetCityById(int id)
        {
            return await _cityServiceImp.GetCityByIdAsync(id);
        }
        [HttpPost]
        public async Task<CityCreateDataDTO> CreateAsync(CityCreateDTO cityCreateDTO)
        {
            return await _cityServiceImp.CreateCity(cityCreateDTO);
        }
        [HttpPut]
        public async Task<CityUpdateDataDTO> UpdateAsync(CityUpdateDTO city)
        {
            return await _cityServiceImp.UpdateCity(city);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(int id)
        {
            if (id > 0)
            {
                var result = await _cityServiceImp.DeleteCityAsync(id);
                if (result)
                {
                    return Ok("Delete Success!");
                }
                else return BadRequest("Delete Failed!");
            };
            return NotFound();
        }
    }
}
