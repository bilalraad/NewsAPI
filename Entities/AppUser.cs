using System.Text.Json.Serialization;
using NewsAPI.Entities;
using NewsAPI.Interfaces;

namespace NewsAPI;

public class AppUser : BaseEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public Gender Gender { get; set; }
    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }

    public List<NewsLike> LikedNews { get; set; } = [];

}


[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Gender
{
    Male,
    Female
}

