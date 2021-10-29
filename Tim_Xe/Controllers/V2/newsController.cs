using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Service.NewsService;

namespace Tim_Xe.API.Controllers.V2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class newsController : ControllerBase
    {
        private readonly NewsSeviceImp _newsServiceImp;
        public newsController()
        {
            _newsServiceImp = new NewsSeviceImp();
        }
        [HttpGet]
        public async Task<NewsListDataDTO> GetAll()
        {
            return await _newsServiceImp.GetAllNewsAsync();
        }
        [HttpGet("{id}")]
        public async Task<NewsDataDTO> GetNewsById(int id)
        {
            return await _newsServiceImp.GetNewsByIdAsync(id);
        }
        [HttpPost]
        public async Task<NewsCreateDataDTO> CreateAsync(NewsCreateDTO news)
        {
            return await _newsServiceImp.CreateNews(news);
        }
        [HttpPut]
        public async Task<NewsUpdateDataDTO> UpdateAsync(NewsUpdateDTO news)
        {
            return await _newsServiceImp.UpdateNews(news);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteAsync(int id)
        {
            if (id != 0)
            {
                var resutl = await _newsServiceImp.DeleteNewsAsync(id);
                if (resutl)
                {
                    return Ok("Delete Success!");
                }
                else return BadRequest("Delete Failed!");
            };
            return NotFound();

        }
    }
}
