using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAPI.DTOs;
using NewsAPI.Interfaces;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NewsAPI.Controllers
{

    public class AuthController : BaseController
    {
        private readonly Context _context;
        private readonly ITokenService _tokenService;

        public AuthController(Context context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            if (await isUserExists(registerDto.Email))
            {
                return BadRequest("User already exists");
            }

            using HMACSHA512 hmac = new HMACSHA512();
            User user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok(
                new UserDto()
                {
                    Name = user.Name,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user),
                }
            );
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(LoginDto loginDto)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null) return Unauthorized();

            using HMACSHA512 hmac = new HMACSHA512(user.PasswordSalt);
            byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            if (!computedHash.SequenceEqual(user.PasswordHash)) return Unauthorized();

            return Ok(
                new UserDto()
                {
                    Name = user.Name,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user),
                }
            );

        }

        private async Task<bool> isUserExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);

        }

    }



}