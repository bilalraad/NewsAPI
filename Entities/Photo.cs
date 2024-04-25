using System.ComponentModel.DataAnnotations.Schema;

namespace NewsAPI.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        public required string Url { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsMain { get; set; } = false;
        public required string PublicId { get; set; }
        public int NewsId { get; set; }
        public required News News { get; set; }
    }
}