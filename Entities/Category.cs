using System.ComponentModel.DataAnnotations.Schema;
using NewsAPI.Interfaces;

namespace NewsAPI.Entities;

[Table("Categories")]
public class Category : BaseEntity
{
    public required string Name { get; set; }

    public string? Image { get; set; }

    public List<News> News { get; set; } = new();
}
