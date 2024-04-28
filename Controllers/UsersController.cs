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
        return await _userRepository.GetUserById(id);

    }

}
