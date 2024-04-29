using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;

namespace NewsAPI.Interfaces
{
    public interface IUserRepository
    {

        Task<UserDto?> GetUserById(int id);
        Task<UserDto?> GetUserByEmail(string email);
        Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers();
        Task<ActionResult> AddUser(RegisterDto user);
        Task<ActionResult> UpdateUser(int id, UpdateUserDto user);
        Task<ActionResult> DeleteUser(int id);
    }

}