using System.ComponentModel.DataAnnotations.Schema;
using NewsAPI.Interfaces;

namespace NewsAPI;

public class AppUser : BaseEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public Gender Gender { get; set; }
    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }

}

public enum Gender
{
    Male,
    Female
}

