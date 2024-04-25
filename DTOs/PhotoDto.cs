namespace NewsAPI.DTOs
{
    public class PhotoDto
    {
        public required string Url { get; set; }
        public required string PublicId { get; set; }
        public string? Description { get; set; }
        public bool IsMain { get; set; } = false;

    }
}