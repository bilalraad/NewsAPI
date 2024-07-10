using NewsAPI.Interfaces;

namespace NewsAPI.Entities;

public class Photo : IEntity
{
    public required string Url { get; set; }
    public string? Description { get; set; }
    public bool IsMain { get; set; } = false;
    public required string PublicId { get; set; }
}
