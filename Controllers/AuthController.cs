using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAPI.DTOs;
using NewsAPI.Interfaces;

namespace NewsAPI.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthDto>> Register(RegisterDto registerDto)
        {

            return await _authRepository.RegisterAsync(registerDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthDto>> Login(LoginDto loginDto)
        {
            return await _authRepository.LoginAsync(loginDto);
        }



    }



}