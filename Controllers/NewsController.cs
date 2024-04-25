using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAPI.DTOs;
using NewsAPI.Entities;

namespace NewsAPI.Controllers
{
    [Authorize]
    public class NewsController : BaseController
    {
        private readonly Context _context;
        private readonly ILogger<NewsController> _logger;

        public NewsController(Context context, ILogger<NewsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<News>>> GetAll()
        {
            IEnumerable<News> news = await _context.News.ToListAsync();
            return Ok(news);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<News>> GetById(int id)
        {
            var news = await _context.News.FirstOrDefaultAsync(n => n.Id == id);
            if (news == null) return NotFound();

            return Ok(news);
        }

        [HttpPost]
        public async Task<ActionResult<News>> Create(CreateNewsDto newsDto)
        {
            News news = new News
            {
                Title = newsDto.Title,
                Content = newsDto.Content,
                Author = newsDto.AuthorId.ToString(),
                // photos = newsDto.Photos.Select(p => new Photo { Url = p.Url, IsMain = p.IsMain, PublicId = p.PublicId, }).ToList(),
                tags = newsDto.Tags,
            };
            await _context.News.AddAsync(news);

            _context.SaveChanges();
            return Ok(news);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<NoContentResult>> Update(int id, CreateNewsDto updatedNewsDto)
        {
            News? news = await _context.News.FirstOrDefaultAsync(n => n.Id == id);
            if (news == null) return NotFound();

            news.Title = updatedNewsDto.Title;
            news.Content = updatedNewsDto.Content;
            // if (updatedNewsDto.Photos?.Count > 0)
            // news.photos = updatedNewsDto.Photos;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<NoContentResult>> Delete(int id)
        {
            var news = await _context.News.FirstOrDefaultAsync(n => n.Id == id);
            if (news == null) return NotFound();

            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}