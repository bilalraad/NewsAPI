using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsAPI.DTOs;
using NewsAPI.Entities;
using NewsAPI.Errors;
using NewsAPI.Helpers;
using NewsAPI.Interfaces;
using NewsAPI.Middlewares;

namespace NewsAPI.Repositories
{
    public class LikesRepository : ILikesRepository
    {
        private readonly Context _context;

        private readonly IMapper _mapper;


        public LikesRepository(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }


        public async Task<PaginatedList<NewsDto>> GetLikedNews(Guid userId, PagingDto pagingDto)
        {
            var query = _context.NewsLikes.Include(n => n.TargetNews).Where(n => n.SourceUserId == userId).Select(n => n.TargetNews).AsNoTracking();
            var likedNews = await query.PaginateAsync(pagingDto);
            var likedNewsDtos = _mapper.MapPaginatedList<News, NewsDto>(likedNews);
            return likedNewsDtos;
        }

        public async Task<int> GetNewsLikeCountAsync(Guid newsId)
        {
            var news = await _context.News.FindAsync(newsId);
            if (news == null) throw AppException.NotFound("News not found");
            return news.LikedByUsersCount;
        }

        public async Task<PaginatedList<UserDto>> GetNewsLikesAsync(Guid newsId, PagingDto pagingDto)
        {
            var query = _context.NewsLikes.Include(nl => nl.SourceUser).Where(nl => nl.TargetNewsId == newsId).Select(nl => nl.SourceUser).AsNoTracking();
            var users = await query.PaginateAsync(pagingDto);
            var userDtos = _mapper.MapPaginatedList<AppUser, UserDto>(users!);
            return userDtos;
        }

        public async Task AddToLikedNews(Guid userId, Guid newsId)
        {
            NewsLike? like = await _context.NewsLikes.FirstOrDefaultAsync(nl => nl.SourceUserId == userId && nl.TargetNewsId == newsId);
            if (like != null) return;

            var news = await _context.News.FindAsync(newsId);
            if (news == null) throw AppException.NotFound("News not found");
            var user = await _context.Users.FindAsync(userId);
            if (user == null) throw AppException.NotFound("User not found");
            var newsLike = new NewsLike
            {
                SourceUserId = userId,
                TargetNewsId = newsId,
                SourceUser = user,
                TargetNews = news,
            };
            _context.NewsLikes.Add(newsLike);
            news.LikedByUsersCount++;
            await _context.SaveChangesAsync();
        }


        public async Task RemoveFromLikedNews(Guid userId, Guid newsId)
        {
            NewsLike? newsLike = await _context.NewsLikes.FirstOrDefaultAsync(nl => nl.SourceUserId == userId && nl.TargetNewsId == newsId);
            if (newsLike == null) throw AppException.NotFound("You have not liked this news");
            _context.NewsLikes.Remove(newsLike);
            News? news = await _context.News.FindAsync(newsId);
            news!.LikedByUsersCount--;
            await _context.SaveChangesAsync();

        }
    }
}