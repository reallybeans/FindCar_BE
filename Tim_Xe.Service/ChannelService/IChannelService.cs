using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;

namespace Tim_Xe.Service.ChannelService
{
    public interface IChannelService
    {
        Task<ChannelListDataDTO> GetAllChannelsAsync();
        Task<ChannelCreateDataDTO> CreateChannel(ChannelCreateDTO channel);
        Task<ChannelUpdateDataDTO> UpdateChannel(ChannelUpdateDTO channel);
        Task<bool> DeleteChannelAsync(int id);
    }
}
