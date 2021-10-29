using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;

namespace Tim_Xe.Service.NewsService
{
    public interface INewsService
    {
        Task<NewsListDataDTO> GetAllNewsAsync();
        Task<NewsDataDTO> GetNewsByIdAsync(int id);
        Task<NewsCreateDataDTO> CreateNews(NewsCreateDTO news);
        Task<NewsUpdateDataDTO> UpdateNews(NewsUpdateDTO news);
        Task<bool> DeleteNewsAsync(int id);
    }
}
