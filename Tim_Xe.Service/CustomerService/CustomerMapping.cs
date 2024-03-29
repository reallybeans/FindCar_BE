﻿using AutoMapper;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Service.CustomerService
{
    public class CustomerMapping : Profile
    {
        public MapperConfiguration configCustomer = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Customer, CustomerDTO>();
        });
    }
}
