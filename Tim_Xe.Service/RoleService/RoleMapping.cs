﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Service.RoleService
{
    public class RoleMapping : Profile
    {
        public MapperConfiguration configRole = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Role, RoleDTO>();
        });
    }
}
