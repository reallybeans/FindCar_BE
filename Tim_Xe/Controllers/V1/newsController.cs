using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Service.NewsService;

namespace Tim_Xe.API.Controllers.V1
{
    [Route("api/v1/news")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly NewsSeviceImp _newsServiceImp;
        public NewsController()
        {
            _newsServiceImp = new NewsSeviceImp();
        }
        [HttpGet]
        public async Task<IEnumerable<NewsDTO>> GetAll()
        {
            return await _newsServiceImp.GetAllNewsAsync();
        }
        [HttpGet("{id}")]
        public async Task<NewsDTO> GetNewsById(int id)
        {
            return await _newsServiceImp.GetNewsByIdAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult<int>> CreateAsync(NewsCreateDTO news)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data!");
            if (await _newsServiceImp.CreateNews(news) == 1)
                return Ok("Create Success!");
            else return BadRequest("Create Failed!");
        }
        [HttpPut]
        public async Task<ActionResult<int>> UpdateAsync(NewsUpdateDTO news)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data!");
            if (await _newsServiceImp.UpdateNews(news) == 1)
                return Ok("Update Success!");
            else return BadRequest("Update Failed!");
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
