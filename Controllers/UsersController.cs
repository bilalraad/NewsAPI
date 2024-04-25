using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAPI.Controllers;
using NewsAPI.DTOs;

namespace NewsAPI;


[Authorize]
public class UsersController : BaseController
{
    private readonly Context _context;

    public UsersController(Context context)
    {
        _context = context;
    }



    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAllAsync()
    {
        IEnumerable<User> users = await _context.Users.ToListAsync();
        return Ok(users.Select(u => UserDto.FromUser(u)));
    }

    // GET: api/users/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get(int id)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        return user == null ? NotFound() : Ok(UserDto.FromUser(user));
    }


}
