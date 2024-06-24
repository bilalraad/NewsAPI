using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;
using NewsAPI.Entities;
using NewsAPI.Extensions;
using NewsAPI.Helpers;
using NewsAPI.Interfaces;


namespace NewsAPI.Controllers
{
    [Authorize]
    public class NewsLikesController : BaseController
    {

        private readonly ILikesRepository _likesRepository;

        public NewsLikesController(ILikesRepository likesRepository)
        {
            _likesRepository = likesRepository;
        }

        [HttpPost("Like/{newsId}")]
        public async Task<ActionResult> AddToLikedNews(Guid newsId)
        {
            await _likesRepository.AddToLikedNews(User.GetUserId(), newsId);
            return NoContent();
        }

        [HttpPost("Unlike/{newsId}")]
        public async Task<ActionResult> RemoveFromLikedNews(Guid newsId)
        {
            await _likesRepository.RemoveFromLikedNews(User.GetUserId(), newsId);
            return NoContent();
        }

        [HttpGet("{newsId}")]
        public async Task<ActionResult<PaginatedList<UserDto>>> GetNewsLikesAsync(Guid newsId, [FromQuery] PagingDto pagingDto)
        {

            return await _likesRepository.GetNewsLikesAsync(newsId, pagingDto);

        }
        [HttpGet("Count/{newsId}")]
        public async Task<ActionResult<int>> GetNewsLikeCountAsync(Guid newsId)
        {

            return await _likesRepository.GetNewsLikeCountAsync(newsId);
        }

        [HttpGet("UserLikes/{userId}")]
        public async Task<ActionResult<PaginatedList<NewsDto>>> GetLikedNews(Guid userId, [FromQuery] PagingDto pagingDto)
        {
            return await _likesRepository.GetLikedNews(userId, pagingDto);
        }


    }
}