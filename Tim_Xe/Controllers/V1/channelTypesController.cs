﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Service.ChannelTypeService;

namespace Tim_Xe.API.Controllers.V1
{
    [Route("api/v1/channel-types")]
    [ApiController]
    public class ChannelTypesController : ControllerBase
    {
        private readonly ChannelTypeServiceImp _channelTypeServiceImp;
        public ChannelTypesController()
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
