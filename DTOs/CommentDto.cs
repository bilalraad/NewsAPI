namespace NewsAPI.DTOs
{
    public class CommentDto
    {
        public required string Content { get; set; }
        public required int NewsId { get; set; }
        public required int UserId { get; set; }

    }
}