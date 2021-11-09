using AutoMapper;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Service.PriceTimeService
{
    public class PriceTimeMapping : Profile
    {
        public MapperConfiguration configPriceTime = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<PriceTime, PriceTimeDTO>();
        });
    }
}
