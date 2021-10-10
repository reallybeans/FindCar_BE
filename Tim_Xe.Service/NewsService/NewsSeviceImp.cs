using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Service.NewsService
{
    public class NewsSeviceImp
    {
        private readonly TimXeDBContext context;
        private readonly NewsMapping newsMapping;
        public NewsSeviceImp()
        {
            context = new TimXeDBContext();
            newsMapping = new NewsMapping();
        }
        public async Task<IEnumerable<NewsDTO>> GetAllNewsAsync()
        {
            return await context.News.ProjectTo<NewsDTO>(newsMapping.configNews).ToListAsync();
        }
        public async Task<NewsDTO> GetNewsByIdAsync(int id)
        {
            var result = await context.News.ProjectTo<NewsDTO>(newsMapping.configNews).FirstOrDefaultAsync(c => c.Id == id);
            return result;
        }
        public async Task<int> CreateNews(NewsCreateDTO news)
        {
            try
            {
                var extstingGroup = await context.Groups.FindAsync(news.IdGroup);
                if (extstingGroup == null)
                {
                    return 0;
                }
                else
                {
                    context.News.Add(new News()
                    {
                        Name = news.Name,
                        Content = news.Content,
                        IdGroup = news.IdGroup,
                        IsDeleted = false,
                    });
                }
            }
            catch(Exception e)
            {
                return 0;
            }
            return await context.SaveChangesAsync();
        }
        public async Task<int> UpdateNews(NewsUpdateDTO news)
        {
            var existingNews = await context.News.FirstOrDefaultAsync(c => c.Id == news.Id);
            var extstingGroup = await context.Groups.FindAsync(news.IdGroup);
            if (extstingGroup == null)
            {
                return 0;
            }
            if (existingNews != null)
            {
                existingNews.Name = news.Name;
                existingNews.Content = news.Content;
                existingNews.IdGroup = news.IdGroup;
                existingNews.IsDeleted = news.IsDeleted;
            }
            else
            {
                return 0;
            }

            return await context.SaveChangesAsync();
        }
        public async Task<bool> DeleteNewsAsync(int id)
        {
            var existingNews = await context.News.FirstOrDefaultAsync(c => c.Id == id);

            if (existingNews != null)
            {
                existingNews.IsDeleted = true;
                await context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }


        }
    }
}
