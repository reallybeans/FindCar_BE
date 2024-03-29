﻿using AutoMapper;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Service.VehicleTypeService
{
    public class VehicleTypeMapping : Profile
    {
        public MapperConfiguration configVehicleType = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<VehicleType, VehicleTypeDTO>();
        });
    }
}
