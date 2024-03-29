﻿using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Service.ChannelTypeService
{
    public class ChannelTypeServiceImp : IChannelTypeService
    {
        private readonly TimXeDBContext context;
        private readonly ChannelTypeMapping channelTypeMapping;
        public ChannelTypeServiceImp()
        {
            context = new TimXeDBContext();
            channelTypeMapping = new ChannelTypeMapping();
        }
        public async Task<ChannelTypesListDataDTO> GetAllChannelTypesAsync()
        {
            var result = await context.ChannelTypes.ProjectTo<ChannelTypeDTO>(channelTypeMapping.configChannelType).ToListAsync();

            if (result.Count() == 0)
            {
                return new ChannelTypesListDataDTO("list is empty", null, "empty");
            }
            else return new ChannelTypesListDataDTO("success", result, "success");
        }
        public async Task<ChannelTypesDataDTO> GetChannelTypeByIdAsync(int id)
        {
            var result = await context.ChannelTypes.ProjectTo<ChannelTypeDTO>(channelTypeMapping.configChannelType).FirstOrDefaultAsync(c => c.Id == id);
            if (result != null)
            {
                return new ChannelTypesDataDTO("success", result, "available");
            }
            else return new ChannelTypesDataDTO("fail", null, "not available");
        }
        public async Task<ChannelTypeCreateDataDTO> CreateChannelType(ChannelTypeCreateDTO channelType)
        {
            try
            {
                context.ChannelTypes.Add(new ChannelType()
                {
                    Name = channelType.Name,
                });
                await context.SaveChangesAsync();
                return new ChannelTypeCreateDataDTO("create success", channelType, "success");
            }
            catch (Exception e)
            {
                return new ChannelTypeCreateDataDTO("create fail", null, "fail");
            }
        }
        public async Task<ChannelTypeUpdateDataDTO> UpdateChannelType(ChannelTypeUpdateDTO channelType)
        {
            try
            {
                var existingChannelType = await context.ChannelTypes.FirstOrDefaultAsync(c => c.Id == channelType.Id);
                if (existingChannelType != null)
                {
                    existingChannelType.Name = channelType.Name;
                    context.ChannelTypes.Update(existingChannelType);
                    await context.SaveChangesAsync();
                    return new ChannelTypeUpdateDataDTO("update success", channelType, "success");
                }
                else
                {
                    return new ChannelTypeUpdateDataDTO("update fail", null, "fail");
                }
            }
            catch (Exception e)
            {
                return new ChannelTypeUpdateDataDTO("update fail", null, "fail");
            }
        }
    }
}
