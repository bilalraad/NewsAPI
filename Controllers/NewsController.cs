using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;
using NewsAPI.Helpers;
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
        public async Task<ActionResult<IEnumerable<NewsDto>>> GetAll([FromQuery] NewsFilter newsFilter)
        {
            return Ok(await _newsRepository.GetAllNewsAsync(newsFilter));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ServiceFilter(typeof(UpdateNewsViewCount))]
        public async Task<ActionResult<NewsDto>> GetById(Guid id)
        {

            return Ok(await _newsRepository.GetNewsByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateNewsDto newsDto)
        {

            await _newsRepository.AddNewsAsync(newsDto);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, UpdateNewsDto updatedNewsDto)
        {

            await _newsRepository.UpdateNewsAsync(id, updatedNewsDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes a specific News.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _newsRepository.DeleteNewsAsync(id);
            return NoContent();
        }
    }
}