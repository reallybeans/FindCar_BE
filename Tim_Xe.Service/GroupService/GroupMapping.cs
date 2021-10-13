using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Service.GroupService
{
    public class GroupMapping
    {
        public MapperConfiguration configManager = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Group, GroupDTO>();
        });
    }
}
