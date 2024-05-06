using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;
using NewsAPI.Helpers;

namespace NewsAPI.Interfaces
{
    public interface IUserRepository
    {

        Task<UserDto?> GetUserByIdAsync(Guid id);
        Task<UserDto?> GetUserByEmailAsync(string email);
        Task<PaginatedList<UserDto>> GetAllUsersAsync(PagingDto pagingDto);
        Task AddUserAsync(RegisterDto user);
        Task UpdateUserAsync(Guid id, UpdateUserDto user);
        Task DeleteUserAsync(Guid id);
    }

}