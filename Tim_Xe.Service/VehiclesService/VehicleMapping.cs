﻿using AutoMapper;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Service.VehiclesService
{
    public class VehicleMapping : Profile
    {
        public MapperConfiguration configVehicle = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Vehicle, VehicleDTO>();
        });
    }
}
