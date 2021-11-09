using AutoMapper;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Service.NewsService
{
    public class NewsMapping : Profile
    {
        public MapperConfiguration configNews = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<News, NewsDTO>();
        });
    }
}
