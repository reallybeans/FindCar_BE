using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Service.ManagerService
{
    public class ManagerMapping : Profile
    {
        public MapperConfiguration configManager = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Manager, ManagerDTO>();
        });
    }
}
