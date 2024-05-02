namespace NewsAPI.DTOs
{
    public class AuthDto
    {
        public required string Token { get; set; }
        public required UserDto User { get; set; }
    }
}