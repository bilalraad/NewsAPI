using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;
using NewsAPI.Entities;

namespace NewsAPI.Interfaces
{
    public interface INewsRepository
    {
        Task<NewsDto> GetNewsByIdAsync(Guid id);
        Task<IEnumerable<NewsDto>> GetAllNewsAsync();
        Task AddNewsAsync(CreateNewsDto news);
        Task UpdateNewsAsync(Guid id, UpdateNewsDto news);
        Task DeleteNewsAsync(Guid id);
    }
}