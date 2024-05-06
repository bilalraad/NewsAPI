using NewsAPI.DTOs;
using NewsAPI.Interfaces;

namespace NewsAPI.Entities
{
    public class News : BaseEntity
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required Guid AuthorId { get; set; }
        public List<string> PhotosUrls { get; set; } = new();

        public List<string> Tags { get; set; } = new();

        public List<Comment> Comments { get; set; } = new();

        public bool IsPublished { get; set; } = true;


    }
}