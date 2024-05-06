namespace NewsAPI.DTOs
{
    public class NewsFilter : PagingDto
    {

        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Tag { get; set; }

    }
}