using NewsAPI.DTOs;
using NewsAPI.Helpers;

namespace NewsAPI.Interfaces;

public interface ILikesRepository
{
    Task<PaginatedList<UserDto>> GetNewsLikesAsync(Guid newsId, PagingDto pagingDto);

    Task<int> GetNewsLikeCountAsync(Guid newsId);

    Task<PaginatedList<NewsDto>> GetLikedNews(Guid userId, PagingDto pagingDto);

    Task AddToLikedNews(Guid userId, Guid newsId);

    Task RemoveFromLikedNews(Guid userId, Guid newsId);
}
