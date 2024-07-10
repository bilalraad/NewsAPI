namespace NewsAPI.DTOs;

public class CategoryDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Image { get; set; }
}
