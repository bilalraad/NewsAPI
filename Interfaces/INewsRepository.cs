using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;
using NewsAPI.Entities;

namespace NewsAPI.Interfaces
{
    public interface INewsRepository
    {
        Task<ActionResult<NewsDto>> GetNewsById(int id);
        Task<ActionResult<IEnumerable<NewsDto>>> GetAllNews();
        Task<ActionResult> AddNews(CreateNewsDto news);
        Task<ActionResult> UpdateNews(int id, UpdateNewsDto news);
        Task<ActionResult> DeleteNews(int id);


    }
}