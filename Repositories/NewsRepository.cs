using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAPI.DTOs;
using NewsAPI.Entities;
using NewsAPI.Interfaces;

namespace NewsAPI.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public NewsRepository(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult> AddNews(CreateNewsDto news)
        {
            News newNews = _mapper.Map<News>(news);
            await _context.News.AddAsync(newNews);
            await _context.SaveChangesAsync();
            return new OkResult();

        }

        public async Task<ActionResult> DeleteNews(int id)
        {
            News? news = await _context.News.FindAsync(id);
            if (news == null) return new NotFoundResult();

            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return new NoContentResult();
        }

        public async Task<ActionResult<IEnumerable<NewsDto>>> GetAllNews()
        {
            return await _context.News
                    .ProjectTo<NewsDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
        }

        public async Task<ActionResult<NewsDto>> GetNewsById(int id)
        {
            NewsDto? news = await _context.News
                  .ProjectTo<NewsDto>(_mapper.ConfigurationProvider)
                  .FirstOrDefaultAsync(u => u.Id == id);
            return news != null ? new OkObjectResult(news) : new NotFoundResult();
        }



        public async Task<ActionResult> UpdateNews(int id, UpdateNewsDto updateNewsDto)
        {
            News? oldNews = await _context.News.FindAsync(id);
            if (oldNews == null) return new NotFoundResult();
            _mapper.Map(updateNewsDto, oldNews);
            await _context.SaveChangesAsync();
            return new NoContentResult();

        }
    }
}