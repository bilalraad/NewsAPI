namespace NewsAPI.DTOs
{
    public class UploadImageDto
    {
        public required IFormFile File { get; set; }

        public string? Description { get; set; }

        public bool IsMain { get; set; } = false;

    }
}