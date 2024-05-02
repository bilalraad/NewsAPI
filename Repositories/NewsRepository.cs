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

        private readonly ILogger<NewsRepository> _logger;

        public NewsRepository(Context context, IMapper mapper, ILogger<NewsRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ActionResult> AddNewsAsync(CreateNewsDto news)
        {
            News newNews = _mapper.Map<News>(news);
            await _context.News.AddAsync(newNews);
            await _context.SaveChangesAsync();
            return new OkResult();

        }

        public async Task<ActionResult> DeleteNewsAsync(Guid id)
        {
            News? news = await _context.News.FindAsync(id);
            if (news == null) return new NotFoundResult();

            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return new NoContentResult();
        }

        public async Task<ActionResult<IEnumerable<NewsDto>>> GetAllNewsAsync()
        {
            List<NewsDto> newsDtos = new();

            var news = await _context.News.ToListAsync();

            foreach (News value in news)
            {
                NewsDto newsDto = _mapper.Map<NewsDto>(value);

                newsDto.Photos = await GetPhotosAsync(value.PhotosUrls);

                newsDtos.Add(newsDto);
            }

            return newsDtos;

        }

        public async Task<ActionResult<NewsDto>> GetNewsByIdAsync(Guid id)
        {
            NewsDto? news = await _context.News
                  .ProjectTo<NewsDto>(_mapper.ConfigurationProvider)
                  .FirstOrDefaultAsync(u => u.Id == id);
            return news != null ? new OkObjectResult(news) : new NotFoundResult();
        }



        public async Task<ActionResult> UpdateNewsAsync(Guid id, UpdateNewsDto updateNewsDto)
        {
            News? oldNews = await _context.News.FindAsync(id);
            if (oldNews == null) return new NotFoundResult();
            _mapper.Map(updateNewsDto, oldNews);
            await _context.SaveChangesAsync();
            return new NoContentResult();

        }

        private async Task<List<PhotoDto>> GetPhotosAsync(List<string> photosUrls)
        {
            List<PhotoDto> photoDtos = new();

            foreach (string photosUrl in photosUrls)
            {

                PhotoDto? photoDto = await _context.Photos
                    .ProjectTo<PhotoDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(p => p.Url == photosUrl);
                if (photoDto != null) photoDtos.Add(photoDto!);
            }
            return photoDtos;
        }
    }
}