using AutoMapper;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Service.PriceKmService
{
    public class PriceKmMapping : Profile
    {
        public MapperConfiguration configPriceKm = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<PriceKm, PriceKmDTO>();
        });
    }
}
