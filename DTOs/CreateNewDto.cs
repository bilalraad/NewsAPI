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
        public required int AuthorId { get; set; }
        public List<PhotoDto> Photos { get; set; } = new();
        public List<string> Tags { get; set; } = new();

        public bool IsPublished { get; set; } = true;



    }
}