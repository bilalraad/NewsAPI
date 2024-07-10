using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;
using NewsAPI.Extensions;
using NewsAPI.Helpers;
using NewsAPI.Interfaces;


namespace NewsAPI.Controllers;

[Authorize]
public class NewsLikesController(IUnitOfWork _unitOfWork) : BaseController
{


    [HttpPost("Like/{newsId}")]
    public async Task<ActionResult> AddToLikedNews(Guid newsId)
    {
        await _unitOfWork.LikesRepository.AddToLikedNews(User.GetUserId(), newsId);
        await _unitOfWork.Complete();
        return NoContent();
    }

    [HttpPost("Unlike/{newsId}")]
    public async Task<ActionResult> RemoveFromLikedNews(Guid newsId)
    {
        await _unitOfWork.LikesRepository.RemoveFromLikedNews(User.GetUserId(), newsId);
        await _unitOfWork.Complete();
        return NoContent();
    }

    [HttpGet("{newsId}")]
    public async Task<ActionResult<PaginatedList<UserDto>>> GetNewsLikesAsync(Guid newsId, [FromQuery] PagingDto pagingDto)
    {

        return await _unitOfWork.LikesRepository.GetNewsLikesAsync(newsId, pagingDto);

    }
    [HttpGet("Count/{newsId}")]
    public async Task<ActionResult<int>> GetNewsLikeCountAsync(Guid newsId)
    {

        return await _unitOfWork.LikesRepository.GetNewsLikeCountAsync(newsId);
    }

    [HttpGet("UserLikes/{userId}")]
    public async Task<ActionResult<PaginatedList<NewsDto>>> GetLikedNews(Guid userId, [FromQuery] PagingDto pagingDto)
    {
        return await _unitOfWork.LikesRepository.GetLikedNews(userId, pagingDto);
    }


}
