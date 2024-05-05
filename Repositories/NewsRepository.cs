using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NewsAPI.DTOs;
using NewsAPI.Entities;
using NewsAPI.Errors;
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

        public async Task AddNewsAsync(CreateNewsDto news)
        {
            News newNews = _mapper.Map<News>(news);
            await _context.News.AddAsync(newNews);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteNewsAsync(Guid id)
        {
            News? news = await _context.News.FindAsync(id);
            if (news == null) throw AppException.NotFound("News not found");
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<NewsDto>> GetAllNewsAsync()
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

        public async Task<NewsDto> GetNewsByIdAsync(Guid id)
        {
            NewsDto? news = await _context.News
                  .ProjectTo<NewsDto>(_mapper.ConfigurationProvider)
                  .FirstOrDefaultAsync(u => u.Id == id);
            return news != null ? news : throw AppException.NotFound("News not found"); ;
        }



        public async Task UpdateNewsAsync(Guid id, UpdateNewsDto updateNewsDto)
        {
            News? oldNews = await _context.News.FindAsync(id);
            if (oldNews == null) throw AppException.NotFound("News not found"); ;
            _mapper.Map(updateNewsDto, oldNews);
            await _context.SaveChangesAsync();
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