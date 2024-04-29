using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Controllers;
using NewsAPI.DTOs;
using NewsAPI.Interfaces;

namespace NewsAPI;


[Authorize]
public class UsersController : BaseController
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllAsync()
    {
        return await _userRepository.GetAllUsers();

    }

    // GET: api/users/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> Get(int id)
    {
        var user = await _userRepository.GetUserById(id);
        if (user == null) return NotFound();
        return user;

    }

    // PutL api/users/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateUserDto updatedUserDto)
    {
        return await _userRepository.UpdateUser(id, updatedUserDto);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateProfile(UpdateUserDto updatedUserDto)
    {
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        if (userEmail == null) return NotFound();
        UserDto? user = await _userRepository.GetUserByEmail(userEmail);
        if (user == null) return NotFound();
        return await _userRepository.UpdateUser(user.Id, updatedUserDto);
    }

}
