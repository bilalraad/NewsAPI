using AutoMapper;
using NewsAPI.DTOs;
using NewsAPI.Entities;

namespace NewsAPI.Helpers
{
    public class AutoMapper : Profile
    {

        public AutoMapper()
        {
            CreateMap<News, NewsDto>();
            CreateMap<Photo, PhotoDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<CreateNewsDto, News>();
            CreateMap<UpdateNewsDto, News>();
            CreateMap<User, UserDto>();
            CreateMap<RegisterDto, User>();
            CreateMap<UpdateUserDto, User>();
        }


    }
}