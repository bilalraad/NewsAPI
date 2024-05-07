using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NewsAPI.DTOs;
using NewsAPI.Entities;
using NewsAPI.Errors;
using NewsAPI.Helpers;
using NewsAPI.Interfaces;
using NewsAPI.Middlewares;

namespace NewsAPI.Repositories;

public class NewsRepository : INewsRepository
{
    private readonly Context _context;
    private readonly IMapper _mapper;

    private readonly IUploadRepository _uploadRepository;

    public NewsRepository(Context context, IMapper mapper, IUploadRepository uploadRepository)

    {
        _context = context;
        _mapper = mapper;
        _uploadRepository = uploadRepository;
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

    public async Task<PaginatedList<NewsDto>> GetAllNewsAsync(PagingDto pagingDto)
    {

        var news = await _context.News
        .Where(n => n.DeletedAt == null || DateTime.Compare(DateTime.Now, n.DeletedAt.Value) < 0)
        .Where(n => n.IsPublished)
        .AsNoTracking()
        .PaginateAsync(pagingDto);

        var newsDtos = _mapper.MapPaginatedList<News, NewsDto>(news);

        for (int i = 0; i <= newsDtos.Data.Count - 1; i++)
        {
            newsDtos.Data[i].Photos = await _uploadRepository.GetPhotosByUrls(news.Data[i].PhotosUrls);
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



}
