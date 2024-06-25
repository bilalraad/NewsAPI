using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsAPI.DTOs;
using NewsAPI.Errors;
using NewsAPI.Interfaces;

namespace NewsAPI.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthRepository(UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<bool> isUserExistsAsync(string email)
        {
            return await _userManager.Users.AnyAsync(u => u.NormalizedEmail == email.ToUpper());
        }

        public async Task<AuthDto> LoginAsync(LoginDto loginDto)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == loginDto.Email.ToUpper());

            if (user == null) throw AppException.NotFound("User not found");


            return
                new AuthDto()
                {
                    User = _mapper.Map<UserDto>(user),
                    Token = await _tokenService.CreateToken(user),
                };

        }

        public async Task<AuthDto> RegisterAsync(RegisterDto registerDto)
        {
            if (await isUserExistsAsync(registerDto.Email))
            {
                throw AppException.NotFound("User already exists");
            }

            AppUser user = new AppUser
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                UserName = registerDto.Email,
            };

            var results = await _userManager.CreateAsync(user, registerDto.Password);

            if (!results.Succeeded)
            {
                throw AppException.BadRequest(results.Errors.FirstOrDefault()?.Description ?? "Unknown error");
            }

            return
                new AuthDto()
                {
                    User = _mapper.Map<UserDto>(user),
                    Token = await _tokenService.CreateToken(user),
                };
        }
    }
}