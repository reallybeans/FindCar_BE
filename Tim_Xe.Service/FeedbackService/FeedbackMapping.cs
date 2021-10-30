using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Service.FeedbackService
{
    public class FeedbackMapping : Profile
    {
        public MapperConfiguration configFeedback = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Feedback, FeedbackDTO>();
        });
    }
}
