using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Service.ChannelTypeService;

namespace Tim_Xe.API.Controllers.V1
{
    [Route("api/v1/channelTypes")]
    [ApiController]
    public class ChannelTypesController : ControllerBase
    {
        private readonly ChannelTypeServiceImp _channelTypeServiceImp;
        public ChannelTypesController()
        {
            _channelTypeServiceImp = new ChannelTypeServiceImp();
        }
        [HttpGet]
        public async Task<IEnumerable<ChannelTypeDTO>> GetAll()
        {
            return await _channelTypeServiceImp.GetAllChannelTypesAsync();
        }
        [HttpGet("{id}")]
        public async Task<ChannelTypeDTO> GetChannelTypeById(int id)
        {
            return await _channelTypeServiceImp.GetChannelTypeByIdAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult<int>> CreateAsync(ChannelTypeCreateDTO channelType)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data!");
            if (await _channelTypeServiceImp.CreateChannelType(channelType) == 1)
                return Ok("Create Success!");
            else return BadRequest("Create Failed!");
        }
        [HttpPut]
        public async Task<ActionResult<int>> UpdateAsync(ChannelTypeUpdateDTO channel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data!");
            if (await _channelTypeServiceImp.UpdateChannelType(channel) == 1)
                return Ok("Update Success!");
            else return BadRequest("Update Failed!");
        }
    }
}
