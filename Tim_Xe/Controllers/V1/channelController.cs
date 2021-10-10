using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Service.ChannelService;

namespace Tim_Xe.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class channelController : ControllerBase
    {
        private readonly ChannelServiceImp _channelServiceImp;
        public channelController()
        {
            _channelServiceImp = new ChannelServiceImp();
        }
        [HttpGet]
        public async Task<IEnumerable<ChannelDTO>> GetAll()
        {
            return await _channelServiceImp.GetAllChannelsAsync();
        }
        [HttpGet("{id}")]
        public async Task<ChannelDTO> GetChannelById(int id)
        {
            return await _channelServiceImp.GetChannelByIdAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult<int>> CreateAsync(ChannelCreateDTO channel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data!");
            if (await _channelServiceImp.CreateChannel(channel) == 1)
                return Ok("Create Success!");
            else return BadRequest("Create Failed!");
        }
        [HttpPut]
        public async Task<ActionResult<int>> UpdateAsync(ChannelUpdateDTO channel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data!");
            if (await _channelServiceImp.UpdateChannel(channel) == 1)
                return Ok("Update Success!");
            else return BadRequest("Update Failed!");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteAsync(int id)
        {
            if (id != 0)
            {
                var resutl = await _channelServiceImp.DeleteChannelAsync(id);
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
