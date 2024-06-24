namespace NewsAPI.Entities
{
    public class NewsLike
    {
        public required AppUser SourceUser { get; set; }
        public required Guid SourceUserId { get; set; }

        public required News TargetNews { get; set; }

        public required Guid TargetNewsId { get; set; }

    }
}