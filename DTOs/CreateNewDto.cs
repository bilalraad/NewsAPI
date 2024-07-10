using System.ComponentModel.DataAnnotations;

namespace NewsAPI.DTOs
{
    public class CreateNewsDto
    {
        [Required]
        public required string Title { get; set; }
        [Required]
        public required string Content { get; set; }
        [Required]
        public required Guid AuthorId { get; set; }
        [Required]
        public List<PhotoDto>? Photos { get; set; }
        public List<string> Tags { get; set; } = new();
        public bool IsPublished { get; set; } = true;

        [Required]
        public required Guid CategoryId { get; set; }

    }
}