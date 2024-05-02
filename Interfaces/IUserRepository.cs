using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;

namespace NewsAPI.Interfaces
{
    public interface IUserRepository
    {

        Task<UserDto?> GetUserByIdAsync(Guid id);
        Task<UserDto?> GetUserByEmailAsync(string email);
        Task<ActionResult<IEnumerable<UserDto>>> GetAllUsersAsync();
        Task<ActionResult> AddUserAsync(RegisterDto user);
        Task<ActionResult> UpdateUserAsync(Guid id, UpdateUserDto user);
        Task<ActionResult> DeleteUserAsync(Guid id);
    }

}