using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;
using NewsAPI.Entities;
using NewsAPI.Helpers;

namespace NewsAPI.Interfaces
{
    public interface INewsRepository
    {
        Task<NewsDto> GetNewsByIdAsync(Guid id);
        Task<PaginatedList<NewsDto>> GetAllNewsAsync(PagingDto pagingDto);
        Task AddNewsAsync(CreateNewsDto news);
        Task UpdateNewsAsync(Guid id, UpdateNewsDto news);
        Task DeleteNewsAsync(Guid id);
    }
}