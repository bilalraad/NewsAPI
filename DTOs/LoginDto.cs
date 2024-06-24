using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NewsAPI.DTOs
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        [DefaultValue("sarahanderson@example.com")]
        public required string Email { get; set; }
        [Required]
        [DefaultValue("password@123s")]
        public required string Password { get; set; }
    }
}