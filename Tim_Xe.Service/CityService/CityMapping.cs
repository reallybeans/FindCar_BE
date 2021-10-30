using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Service.CityService
{
    public class CityMapping : Profile
    {
        public MapperConfiguration configCity = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<City, CityDTO>();
        });
    }
}
