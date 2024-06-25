using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Controllers;
using NewsAPI.DTOs;
using NewsAPI.Entities;
using NewsAPI.Extensions;
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
    [Authorize(policy: AppPolicy.RequireAdminRole)]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll([FromQuery] PagingDto pagingDto)
    {
        return Ok(await _userRepository.GetAllUsersAsync(pagingDto));

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
        await _userRepository.UpdateUserAsync(User.GetUserId(), updatedUserDto);
        return NoContent();
    }

}
