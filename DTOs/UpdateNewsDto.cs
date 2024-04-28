namespace NewsAPI.DTOs
{
    public class UpdateNewsDto
    {

        public string? Title { get; set; }
        public string? Content { get; set; }
        public List<PhotoDto>? Photos { get; set; }
        public List<string>? Tags { get; set; }
        public bool? IsPublished { get; set; }
    }
}