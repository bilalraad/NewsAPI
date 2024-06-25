
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using NewsAPI.DTOs;
using NewsAPI.Entities;

namespace NewsAPI.Data;

public class Seed
{

    public static async Task UsersSeed(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        if (!userManager.Users.Any())
        {
            var usersData = await File.ReadAllTextAsync("Data/Seed/UsersSeed.json");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var users = JsonSerializer.Deserialize<List<RegisterDto>>(usersData, options);

            var roles = AppRoles.GetValues<AppRoles>().Select(r => new AppRole { Name = r.ToString() }).ToList();

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }


            foreach (var userData in users ?? [])
            {
                AppUser user = new AppUser
                {
                    Name = userData.Name,
                    Email = userData.Email,
                    UserName = userData.Email,

                };
                await userManager.CreateAsync(user, "password@123s");
                await userManager.AddToRoleAsync(user, "Member");
            }

            AppUser admin = new AppUser
            {
                Name = "Admin",
                Email = "admin@email.news",
                UserName = "admin@email.news",

            };
            await userManager.CreateAsync(admin, "password@123s");
            await userManager.AddToRolesAsync(admin, ["Admin", "Moderator"]);



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
                    IsPublished = true,
                    ViewCount = new Random().Next(1, 100),
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
