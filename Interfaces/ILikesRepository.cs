using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsAPI.DTOs;
using NewsAPI.Entities;
using NewsAPI.Helpers;

namespace NewsAPI.Interfaces
{
    public interface ILikesRepository
    {
        Task<PaginatedList<UserDto>> GetNewsLikesAsync(Guid newsId, PagingDto pagingDto);

        Task<int> GetNewsLikeCountAsync(Guid newsId);

        Task<PaginatedList<NewsDto>> GetLikedNews(Guid userId, PagingDto pagingDto);

        Task AddToLikedNews(Guid userId, Guid newsId);

        Task RemoveFromLikedNews(Guid userId, Guid newsId);
    }
}