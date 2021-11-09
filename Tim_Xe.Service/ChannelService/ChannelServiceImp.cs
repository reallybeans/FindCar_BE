using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository;

namespace Tim_Xe.Service.ChannelService
{
    public class ChannelServiceImp : IChannelService
    {
        private readonly TimXeDBContext context;
        private readonly ChannelMapping channelMapping;
        public ChannelServiceImp()
        {
            context = new TimXeDBContext();
            channelMapping = new ChannelMapping();
        }
        public async Task<ChannelListDataDTO> GetAllChannelsAsync()
        {
            var result = await context.Channels.ProjectTo<ChannelDTO>(channelMapping.configChannel).ToListAsync();
            if (result.Count() == 0)
            {
                return new ChannelListDataDTO("list is empty", null, "empty");
            }
            else return new ChannelListDataDTO("success", result, "success");
        }
        public async Task<ChannelDataDTO> GetChannelByIdAsync(int id)
        {
            var result = await context.Channels.ProjectTo<ChannelDTO>(channelMapping.configChannel).FirstOrDefaultAsync(c => c.Id == id);
            if (result == null)
            {
                return new ChannelDataDTO("fail", null, "not available");
            }
            else
            {
                return new ChannelDataDTO("success", result, result.Status);
            }
        }
        public async Task<ChannelCreateDataDTO> CreateChannel(ChannelCreateDTO channel)
        {
            try
            {
                var existingChannelType = await context.ChannelTypes.FindAsync(channel.IdChannelType);
                var extstingGroup = await context.Groups.FindAsync(channel.IdGroup);
                if (extstingGroup == null)
                {
                    return new ChannelCreateDataDTO("create fail", null, "fail");
                }
                if (existingChannelType == null)
                {
                    return new ChannelCreateDataDTO("create fail", null, "fail");
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
                    await context.SaveChangesAsync();
                    return new ChannelCreateDataDTO("create success", channel, "success");
                }
            }
            catch (Exception e)
            {
                return new ChannelCreateDataDTO("create fail", null, "fail");
            }
        }
        public async Task<ChannelUpdateDataDTO> UpdateChannel(ChannelUpdateDTO channel)
        {
            try
            {
                var existingChannel = await context.Channels.FirstOrDefaultAsync(c => c.Id == channel.Id);
                var extstingGroup = await context.Groups.FindAsync(channel.IdGroup);
                if (extstingGroup == null)
                {
                    return new ChannelUpdateDataDTO("update fail", null, "fail");
                }
                if (existingChannel != null)
                {
                    existingChannel.Name = channel.Name;
                    existingChannel.Url = channel.Url;
                    existingChannel.IdChannelType = channel.IdChannelType;
                    existingChannel.IdGroup = channel.IdGroup;
                    existingChannel.IsDeleted = channel.IsDeleted;
                    existingChannel.Status = channel.Status;
                    context.Channels.Update(existingChannel);
                    await context.SaveChangesAsync();
                    return new ChannelUpdateDataDTO("update success", channel, "success");
                }
                else
                {
                    return new ChannelUpdateDataDTO("update fail", null, "fail");
                }
            }
            catch (Exception e)
            {
                return new ChannelUpdateDataDTO("update fail", null, "fail");
            }
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
