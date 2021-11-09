using AutoMapper;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Service.DriverService
{
    public class DriverMapping : Profile
    {
        public MapperConfiguration configDriver = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Driver, DriverDTO>();
        });
    }
}
