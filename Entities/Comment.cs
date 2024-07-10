using System.ComponentModel.DataAnnotations.Schema;
using NewsAPI.Interfaces;

namespace NewsAPI.Entities;

[Table("Comments")]
public class Comment : IEntity
{
    public required string Content { get; set; }

    public Guid NewsId { get; set; }
    public required News News { get; set; }

    public Guid UserId { get; set; }
    public required AppUser User { get; set; }
}
