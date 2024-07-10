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
public class UsersController(IUnitOfWork _unitOfWork) : BaseController
{


    [HttpGet]
    [Authorize(policy: AppPolicy.RequireAdminRole)]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll([FromQuery] PagingDto pagingDto)
    {
        return Ok(await _unitOfWork.UserRepository.GetAllUsersAsync(pagingDto));

    }

    // GET: api/users/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> Get(Guid id)
    {
        var user = await _unitOfWork.UserRepository.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        return user;

    }


    [HttpPut]
    public async Task<ActionResult> UpdateProfile(UpdateUserDto updatedUserDto)
    {
        await _unitOfWork.UserRepository.UpdateUserAsync(User.GetUserId(), updatedUserDto);
        return NoContent();
    }

}
