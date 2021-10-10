using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Service.ChannelTypeService
{
    public class ChannelTypeServiceImp
    {
        private readonly TimXeDBContext context;
        private readonly ChannelTypeMapping channelTypeMapping;
        public ChannelTypeServiceImp()
        {
            context = new TimXeDBContext();
            channelTypeMapping = new ChannelTypeMapping();
        }
        public async Task<IEnumerable<ChannelTypeDTO>> GetAllChannelTypesAsync()
        {
            return await context.ChannelTypes.ProjectTo<ChannelTypeDTO>(channelTypeMapping.configChannelType).ToListAsync();
        }
        public async Task<ChannelTypeDTO> GetChannelTypeByIdAsync(int id)
        {
            var result = await context.ChannelTypes.ProjectTo<ChannelTypeDTO>(channelTypeMapping.configChannelType).FirstOrDefaultAsync(c => c.Id == id);
            return result;
        }
        public async Task<int> CreateChannelType(ChannelTypeCreateDTO channelType)
        {
            try
            {
                context.ChannelTypes.Add(new ChannelType()
                {
                    Name = channelType.Name,
                });
            }
            catch(Exception e)
            {
                return 0;
            }
            return await context.SaveChangesAsync();
        }
        public async Task<int> UpdateChannelType (ChannelTypeUpdateDTO channelType)
        {
            try
            {
                var existingChannelType = await context.ChannelTypes.FirstOrDefaultAsync(c => c.Id == channelType.Id);
                if (existingChannelType != null)
                {
                    existingChannelType.Name = channelType.Name;
                }
                else
                {
                    return 0;
                }
            }
            catch(Exception e)
            {
                return 0;
            }
            return await context.SaveChangesAsync();
        }
    }
}
