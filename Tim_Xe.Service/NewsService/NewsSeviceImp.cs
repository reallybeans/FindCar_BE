using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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
        public async Task<NewsListDataDTO> GetAllNewsAsync()
        {
            var result = await context.News.ProjectTo<NewsDTO>(newsMapping.configNews).ToListAsync();
            if (result.Count() == 0)
            {
                return new NewsListDataDTO("list is empty", null, "empty");
            }
            else return new NewsListDataDTO("success", result, "success");
        }
        public async Task<NewsDataDTO> GetNewsByIdAsync(int id)
        {
            var result = await context.News.ProjectTo<NewsDTO>(newsMapping.configNews).FirstOrDefaultAsync(c => c.Id == id);
            if (result == null)
            {
                return new NewsDataDTO("fail", result, "not available");
            }
            else
            {
                String status;
                if (result.IsDeleted == true)
                {
                    status = "inActive";
                }
                else
                {
                    status = "active";
                }
                return new NewsDataDTO("success", result, status);
            }
        }
        public async Task<NewsCreateDataDTO> CreateNews(NewsCreateDTO news)
        {
            try
            {
                var extstingGroup = await context.Groups.FindAsync(news.IdGroup);
                if (extstingGroup == null)
                {
                    return new NewsCreateDataDTO("create fail", null, "fail");
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
                    await context.SaveChangesAsync();
                    return new NewsCreateDataDTO("create success", news, "success");
                }
            }
            catch (Exception e)
            {
                return new NewsCreateDataDTO("create fail", null, "fail");
            }
        }
        public async Task<NewsUpdateDataDTO> UpdateNews(NewsUpdateDTO news)
        {
            var existingNews = await context.News.FirstOrDefaultAsync(c => c.Id == news.Id);
            var extstingGroup = await context.Groups.FindAsync(news.IdGroup);
            if (extstingGroup == null)
            {
                return new NewsUpdateDataDTO("update fail", null, "fail");
            }
            if (existingNews != null)
            {
                existingNews.Name = news.Name;
                existingNews.Content = news.Content;
                existingNews.IdGroup = news.IdGroup;
                existingNews.IsDeleted = news.IsDeleted;
                context.News.Update(existingNews);
                await context.SaveChangesAsync();
                return new NewsUpdateDataDTO("update success", news, "success");
            }
            else
            {
                return new NewsUpdateDataDTO("update fail", null, "fail");
            }
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
