namespace NewsAPI.DTOs
{
    public class UserDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Token { get; set; }



        public static UserDto FromUser(User user)
        {
            return new UserDto
            {
                Name = user.Name,
                Email = user.Email,
            };
        }

    }
}