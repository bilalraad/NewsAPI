using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;

namespace NewsAPI.Interfaces
{
    public interface IAuthRepository
    {
        Task<ActionResult<AuthDto>> Register(RegisterDto user);
        Task<ActionResult<AuthDto>> Login(LoginDto user);

        Task<bool> isUserExists(string email);
    }
}