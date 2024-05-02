using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;

namespace NewsAPI.Interfaces
{
    public interface IAuthRepository
    {
        Task<ActionResult<AuthDto>> RegisterAsync(RegisterDto user);
        Task<ActionResult<AuthDto>> LoginAsync(LoginDto user);

        Task<bool> isUserExistsAsync(string email);
    }
}