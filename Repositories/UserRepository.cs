using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAPI.DTOs;
using NewsAPI.Interfaces;

namespace NewsAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UserRepository(Context context, IMapper mapper, ITokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
        }
        public async Task<ActionResult> AddUser(RegisterDto registerDto)
        {
            AppUser user = _mapper.Map<AppUser>(registerDto);
            var (hash, salt) = _tokenService.GenerateHash(registerDto.Password);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<ActionResult> DeleteUser(int id)
        {
            AppUser? user = await _context.Users.FindAsync(id);
            if (user == null) return new NotFoundResult();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return new NoContentResult();

        }

        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            return await _context.Users
                    .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

        }

        public async Task<UserDto?> GetUserByEmail(string email)
        {
            UserDto? user = await _context.Users
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<UserDto?> GetUserById(int id)
        {
            UserDto? user = await _context.Users
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<ActionResult> UpdateUser(int id, UpdateUserDto updateUserDto)
        {

            AppUser? oldUser = await _context.Users.FindAsync(id);
            if (oldUser == null) return new NotFoundResult();
            _mapper.Map(updateUserDto, oldUser);
            await _context.SaveChangesAsync();
            return new NoContentResult();
        }


    }
}