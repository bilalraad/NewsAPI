using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NewsAPI.DTOs
{
    public class RegisterDto
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required Gender Gender { get; set; }

    }
}