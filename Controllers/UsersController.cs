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
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        return Ok(await _userRepository.GetAllUsersAsync());

    }

    // GET: api/users/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> Get(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        return user;

    }


    [HttpPut]
    public async Task<ActionResult> UpdateProfile(UpdateUserDto updatedUserDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return NotFound();
        await _userRepository.UpdateUserAsync(Guid.Parse(userId), updatedUserDto);
        return NoContent();
    }

}
