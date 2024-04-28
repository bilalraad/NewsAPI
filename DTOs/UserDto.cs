namespace NewsAPI.DTOs
{
    public class UserDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required Gender Gender { get; set; }
    }
}