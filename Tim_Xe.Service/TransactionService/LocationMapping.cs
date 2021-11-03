using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Service.TransactionService
{
    public class LocationMapping : Profile
    {
        public MapperConfiguration configLocation = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Location, LocationDTO>();
        });
    }
}
