using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;
using NewsAPI.Interfaces;

namespace NewsAPI.Controllers;

public class AuthController(IUnitOfWork _unitOfWork) : IController
{


    [HttpPost("register")]
    public async Task<ActionResult<AuthDto>> RegisterAsync(RegisterDto registerDto)
    {
        return Ok(await _unitOfWork.AuthRepository.RegisterAsync(registerDto));
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthDto>> Login(LoginDto loginDto)
    {
        return Ok(await _unitOfWork.AuthRepository.LoginAsync(loginDto));
    }

}



