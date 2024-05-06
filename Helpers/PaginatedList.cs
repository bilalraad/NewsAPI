namespace NewsAPI.Helpers
{
    public class PaginatedList<T>
    {
        public int Count { get; set; }
        public required List<T> Data { get; set; }

    }
}