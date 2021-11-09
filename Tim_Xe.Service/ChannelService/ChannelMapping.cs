using AutoMapper;
using Tim_Xe.Data.Models;
using Channel = Tim_Xe.Data.Repository.Entities.Channel;

namespace Tim_Xe.Service.ChannelService
{
    public class ChannelMapping : Profile
    {
        public MapperConfiguration configChannel = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Channel, ChannelDTO>();
        });
    }
}
