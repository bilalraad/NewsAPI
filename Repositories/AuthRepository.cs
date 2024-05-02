using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAPI.DTOs;
using NewsAPI.Errors;
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

        public async Task<bool> isUserExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<AuthDto> LoginAsync(LoginDto loginDto)
        {
            AppUser? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null) throw AppException.NotFound("User not found");
            var (computedHash, _) = _tokenService.GenerateHash(loginDto.Password, user.PasswordSalt);

            if (!computedHash.SequenceEqual(user.PasswordHash)) throw AppException.BadRequest("Invalid password");

            return
                new AuthDto()
                {
                    User = _mapper.Map<UserDto>(user),
                    Token = _tokenService.CreateToken(user),
                };

        }

        public async Task<AuthDto> RegisterAsync(RegisterDto registerDto)
        {
            if (await isUserExistsAsync(registerDto.Email))
            {
                throw AppException.NotFound("User already exists");
            }

            var (hash, salt) = _tokenService.GenerateHash(registerDto.Password);
            AppUser user = new AppUser
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                PasswordHash = hash,
                PasswordSalt = salt,
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return
                new AuthDto()
                {
                    User = _mapper.Map<UserDto>(user),
                    Token = _tokenService.CreateToken(user),
                };
        }
    }
}