using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Service.ChannelService;

namespace Tim_Xe.API.Controllers.V2
{
    [Route("api/v2/[controller]")]
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
        public async Task<ChannelDataDTO> GetChannelById(int id)
        {
            return await _channelServiceImp.GetChannelByIdAsync(id);
        }
        [HttpPost]
        public async Task<ChannelCreateDataDTO> CreateAsync(ChannelCreateDTO channel)
        {
            return await _channelServiceImp.CreateChannel(channel);
        }
        [HttpPut]
        public async Task<ChannelUpdateDataDTO> UpdateAsync(ChannelUpdateDTO channel)
        {
            return await _channelServiceImp.UpdateChannel(channel);
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
