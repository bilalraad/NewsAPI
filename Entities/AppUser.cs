using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using NewsAPI.Entities;
using NewsAPI.Interfaces;

namespace NewsAPI;

public class AppUser : IdentityUser<Guid>
{
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public DateTime? DeletedAt { get; set; }
    public required string Name { get; set; }
    public Gender Gender { get; set; }
    public List<NewsLike> LikedNews { get; set; } = [];
    public List<AppUserRole> UserRoles { get; set; } = [];
}


[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Gender
{
    Male,
    Female
}

