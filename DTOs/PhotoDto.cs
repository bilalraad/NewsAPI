namespace NewsAPI.DTOs
{
    public class PhotoDto
    {
        public Guid Id { get; set; }
        public required string Url { get; set; }
        public string? Description { get; set; }
        public bool IsMain { get; set; } = false;

    }
}