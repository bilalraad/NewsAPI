using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;

namespace NewsAPI.Interfaces
{
    public interface IUserRepository
    {

        Task<ActionResult<UserDto>> GetUserById(int id);
        Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers();
        Task<ActionResult> AddUser(RegisterDto user);
        Task<ActionResult> UpdateUser(UpdateUserDto user);
        Task<ActionResult> DeleteUser(int id);
    }

}