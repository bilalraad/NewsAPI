using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;
using NewsAPI.Entities;

namespace NewsAPI.Interfaces
{
    public interface INewsRepository
    {
        Task<ActionResult<NewsDto>> GetNewsByIdAsync(Guid id);
        Task<ActionResult<IEnumerable<NewsDto>>> GetAllNewsAsync();
        Task<ActionResult> AddNewsAsync(CreateNewsDto news);
        Task<ActionResult> UpdateNewsAsync(Guid id, UpdateNewsDto news);
        Task<ActionResult> DeleteNewsAsync(Guid id);
    }
}