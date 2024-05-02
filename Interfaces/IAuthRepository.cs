using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;

namespace NewsAPI.Interfaces
{
    public interface IAuthRepository
    {
        Task<AuthDto> RegisterAsync(RegisterDto user);
        Task<AuthDto> LoginAsync(LoginDto user);

        Task<bool> isUserExistsAsync(string email);
    }
}