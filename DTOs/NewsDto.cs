namespace NewsAPI.DTOs
{
    public class NewsDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required Guid AuthorId { get; set; }
        public required List<PhotoDto> Photos { get; set; }
        public required List<string> Tags { get; set; }
        public required List<CommentDto> Comments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}