using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAPI.DTOs;
using NewsAPI.Interfaces;

namespace NewsAPI.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly Context _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthRepository(Context context, ITokenService tokenService, IMapper mapper)
        {
            _context = context;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<bool> isUserExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<ActionResult<AuthDto>> Login(LoginDto loginDto)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null) return new UnauthorizedResult();
            var (computedHash, _) = _tokenService.GenerateHash(loginDto.Password, user.PasswordSalt);

            if (!computedHash.SequenceEqual(user.PasswordHash)) return new UnauthorizedResult();

            return new OkObjectResult(
                new AuthDto()
                {
                    User = _mapper.Map<UserDto>(user),
                    Token = _tokenService.CreateToken(user),
                }
            );
        }

        public async Task<ActionResult<AuthDto>> Register(RegisterDto registerDto)
        {
            if (await isUserExists(registerDto.Email))
            {
                return new BadRequestObjectResult("User already exists");
            }



            var (hash, salt) = _tokenService.GenerateHash(registerDto.Password);
            User user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                PasswordHash = hash,
                PasswordSalt = salt,
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new OkObjectResult(
                new AuthDto()
                {
                    User = _mapper.Map<UserDto>(user),
                    Token = _tokenService.CreateToken(user),
                }
            );
        }
    }
}