using System.ComponentModel.DataAnnotations.Schema;

namespace NewsAPI.Entities
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; }


        public int NewsId { get; set; }
        public required News News { get; set; }

        public int UserId { get; set; }
        public required User User { get; set; }
    }
}