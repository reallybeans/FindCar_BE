using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository;

namespace Tim_Xe.Service.ChannelService
{
    public class ChannelServiceImp
    {
        private readonly TimXeDBContext context;
        private readonly ChannelMapping channelMapping;
        public ChannelServiceImp()
        {
            context = new TimXeDBContext();
            channelMapping = new ChannelMapping();
        }
        public async Task<IEnumerable<ChannelDTO>> GetAllChannelsAsync()
        {
            return await context.Channels.ProjectTo<ChannelDTO>(channelMapping.configChannel).ToListAsync();
        }
        public async Task<ChannelDTO> GetChannelByIdAsync(int id)
        {
            var result = await context.Channels.ProjectTo<ChannelDTO>(channelMapping.configChannel).FirstOrDefaultAsync(c => c.Id == id);
            return result;
        }
        public async Task<int> CreateChannel(ChannelCreateDTO channel)
        {
            try
            {
                var existingChannelType = await context.ChannelTypes.FindAsync(channel.IdChannelType);
                var extstingGroup = await context.Groups.FindAsync(channel.IdGroup);
                if (extstingGroup == null)
                {
                    return 0;
                }
                if (existingChannelType == null)
                {
                    return 0;
                }
                else
                {
                    context.Channels.Add(new Data.Repository.Entities.Channel()
                    {
                        Name = channel.Name,
                        Url = channel.Url,
                        IdChannelType = channel.IdChannelType,
                        IdGroup = channel.IdGroup,
                        IsDeleted = false,
                        Status = channel.Status,
                    });
                }
            }
            catch(Exception e)
            {
                return 0;
            }
            return await context.SaveChangesAsync();
        }
        public async Task<int> UpdateChannel(ChannelUpdateDTO channel)
        {
            try
            {
                var existingChannel = await context.Channels.FirstOrDefaultAsync(c => c.Id == channel.Id);
                var extstingGroup = await context.Groups.FindAsync(channel.IdGroup);
                if (extstingGroup == null)
                {
                    return 0;
                }
                if (existingChannel != null)
                {
                    existingChannel.Name = channel.Name;
                    existingChannel.Url = channel.Url;
                    existingChannel.IdChannelType = channel.IdChannelType;
                    existingChannel.IdGroup = channel.IdGroup;
                    existingChannel.IsDeleted = channel.IsDeleted;
                    existingChannel.Status = channel.Status;
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
        public async Task<bool> DeleteChannelAsync(int id)
        {
            var existingChanel = await context.Channels.FirstOrDefaultAsync(c => c.Id == id);

            if (existingChanel != null)
            {
                existingChanel.IsDeleted = true;
                await context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }


        }
    }
}
