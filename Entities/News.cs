using NewsAPI.DTOs;
using NewsAPI.Interfaces;

namespace NewsAPI.Entities;
public class News : IEntity
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public int ViewCount { get; set; } = 0;
    public required Guid AuthorId { get; set; }
    public List<string> PhotosUrls { get; set; } = new();

    public List<string> Tags { get; set; } = new();

    public List<Comment> Comments { get; set; } = new();

    public bool IsPublished { get; set; } = true;

    public List<NewsLike> LikedByUsers { get; set; } = [];

    public int LikedByUsersCount { get; set; } = 0;

}
