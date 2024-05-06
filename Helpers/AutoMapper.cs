using AutoMapper;
using NewsAPI.DTOs;
using NewsAPI.Entities;

namespace NewsAPI.Helpers
{
    public class AutoMapper : Profile
    {

        public AutoMapper()
        {
            CreateMap<News, NewsDto>().ForMember(dest => dest.Photos, opt => opt.Ignore());
            CreateMap<Photo, PhotoDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<CreateNewsDto, News>()
            .ForMember(dest => dest.PhotosUrls, opt => opt.MapFrom(
                src => src.Photos!.ConvertAll(p => p.Url).ToList()
            ));
            CreateMap<UpdateNewsDto, News>();
            CreateMap<AppUser, UserDto>();
            CreateMap<RegisterDto, AppUser>();
            CreateMap<UpdateUserDto, AppUser>();

        }


    }

}