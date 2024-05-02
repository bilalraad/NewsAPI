namespace NewsAPI.DTOs
{
    public class CommentDto
    {
        public required string Content { get; set; }
        public required Guid UserId { get; set; }
    }
}