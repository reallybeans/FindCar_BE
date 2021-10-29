using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Service.ChannelTypeService;

namespace Tim_Xe.API.Controllers.V2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class channelTypesController : ControllerBase
    {
        private readonly ChannelTypeServiceImp _channelTypeServiceImp;
        public channelTypesController()
        {
            _channelTypeServiceImp = new ChannelTypeServiceImp();
        }
        [HttpGet]
        public async Task<ChannelTypesListDataDTO> GetAll()
        {
            return await _channelTypeServiceImp.GetAllChannelTypesAsync();
        }
        [HttpGet("{id}")]
        public async Task<ChannelTypesDataDTO> GetChannelTypeById(int id)
        {
            return await _channelTypeServiceImp.GetChannelTypeByIdAsync(id);
        }
        [HttpPost]
        public async Task<ChannelTypeCreateDataDTO> CreateAsync(ChannelTypeCreateDTO channelType)
        {
            return await _channelTypeServiceImp.CreateChannelType(channelType);
        }
        [HttpPut]
        public async Task<ChannelTypeUpdateDataDTO> UpdateAsync(ChannelTypeUpdateDTO channel)
        {
            return await _channelTypeServiceImp.UpdateChannelType(channel);
        }
    }
}
