namespace NewsAPI.Entities
{
    public class News
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required int AuthorId { get; set; }
        public List<Photo> Photos { get; set; } = new();

        public List<string> Tags { get; set; } = new();

        public List<Comment> Comments { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool IsPublished { get; set; } = false;

        public DateTime? DeletedAt { get; set; }
    }
}