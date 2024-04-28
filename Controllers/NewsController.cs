using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;
using NewsAPI.Interfaces;

namespace NewsAPI.Controllers
{
    [Authorize]
    public class NewsController : BaseController
    {
        private readonly INewsRepository _newsRepository;

        public NewsController(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<NewsDto>>> GetAll()
        {
            return await _newsRepository.GetAllNews();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<NewsDto>> GetById(int id)
        {

            return await _newsRepository.GetNewsById(id);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateNewsDto newsDto)
        {

            return await _newsRepository.AddNews(newsDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateNewsDto updatedNewsDto)
        {

            return await _newsRepository.UpdateNews(id, updatedNewsDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await _newsRepository.DeleteNews(id);
        }
    }
}