using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NewsAPI;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
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
        return Ok(users);
    }

    // GET: api/users/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get(int id)
    {
        User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        return Ok(user);
    }

    // POST: api/users
    // [HttpPost]
    // public IActionResult Post([FromBody] User user)
    // {
    //     // TODO: Implement logic to create a new user
    //     return Ok();
    // }


    // DELETE: api/users/{id}
    // [HttpDelete("{id}")]
    // public IActionResult Delete(int id)
    // {
    //     // TODO: Implement logic to delete a user by id
    //     return Ok();
    // }
}
