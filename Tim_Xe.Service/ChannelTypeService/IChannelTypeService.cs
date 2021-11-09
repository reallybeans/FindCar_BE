using System.Threading.Tasks;
using Tim_Xe.Data.Models;

namespace Tim_Xe.Service.ChannelTypeService
{
    public interface IChannelTypeService
    {
        Task<ChannelTypesListDataDTO> GetAllChannelTypesAsync();
        Task<ChannelTypesDataDTO> GetChannelTypeByIdAsync(int id);
        Task<ChannelTypeCreateDataDTO> CreateChannelType(ChannelTypeCreateDTO channelType);
        Task<ChannelTypeUpdateDataDTO> UpdateChannelType(ChannelTypeUpdateDTO channelType);

    }
}
