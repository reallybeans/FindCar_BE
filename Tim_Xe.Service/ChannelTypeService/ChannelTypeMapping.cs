using AutoMapper;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Service.ChannelTypeService
{
    public class ChannelTypeMapping : Profile
    {
        public MapperConfiguration configChannelType = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ChannelType, ChannelTypeDTO>();
        });
    }
}
