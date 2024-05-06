
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using NewsAPI.DTOs;
using NewsAPI.Entities;

namespace NewsAPI.Data
{
    public class Seed
    {

        public static async Task UsersSeed(Context context)
        {
            if (!context.Users.Any())
            {
                var usersData = await File.ReadAllTextAsync("Data/Seed/UsersSeed.json");
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var users = JsonSerializer.Deserialize<List<RegisterDto>>(usersData, options);

                foreach (var userData in users ?? [])
                {
                    using HMACSHA512 hmac = new HMACSHA512();
                    AppUser user = new AppUser
                    {
                        Name = userData.Name,
                        Email = userData.Email,
                        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("password@123s")),
                        PasswordSalt = hmac.Key
                    };
                    await context.Users.AddAsync(user);
                }

                await context.SaveChangesAsync();
            }
        }

        public static async Task NewsSeed(Context context)
        {
            if (!context.News.Any())
            {
                var newsData = await File.ReadAllTextAsync("Data/Seed/NewsSeed.json");
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<CreateNewsDto>? newsList = JsonSerializer.Deserialize<List<CreateNewsDto>>(newsData, options);


                foreach (var newsObj in newsList ?? [])
                {
                    News news = new News
                    {
                        Title = newsObj.Title,
                        Content = newsObj.Content,
                        Tags = newsObj.Tags,
                        AuthorId = Guid.NewGuid(),
                        IsPublished = true
                    };
                    news.PhotosUrls = newsObj.Photos?.ConvertAll(p => p.Url).ToList() ?? new();
                    List<Photo> photos = newsObj.Photos?.ConvertAll(p => new Photo
                    {
                        Url = p.Url,
                        Description = p.Description,
                        IsMain = p.IsMain,
                        PublicId = p.Url,

                    }) ?? new();
                    await context.News.AddAsync(news);
                    await context.Photos.AddRangeAsync(photos);

                }
                await context.SaveChangesAsync();
            }
        }
    }
}