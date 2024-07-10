using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NewsAPI.DTOs;
using NewsAPI.Errors;
using NewsAPI.Helpers;
using NewsAPI.Interfaces;
using NewsAPI.Middlewares;

namespace NewsAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        private readonly IAuthRepository _authRepository;

        public UserRepository(Context context, IMapper mapper,
            IAuthRepository authRepository)
        {
            _context = context;
            _mapper = mapper;
            _authRepository = authRepository;
        }
        public async Task AddUserAsync(RegisterDto registerDto)
        {
            await _authRepository.RegisterAsync(registerDto);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            AppUser? user = await _context.Users.FindAsync(id);
            if (user == null) throw AppException.NotFound("User not found");

            _context.Users.Remove(user);


        }

        public async Task<PaginatedList<UserDto>> GetAllUsersAsync(PagingDto pagingDto)
        {
            return await _context.Users
                    .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                    .AsNoTracking()
                    .PaginateAsync(pagingDto);

        }

        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            UserDto? user = await _context.Users
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) throw AppException.NotFound("User not found");
            return user;
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            UserDto? user = await _context.Users
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) throw AppException.NotFound("User not found");

            return user;
        }

        public async Task UpdateUserAsync(Guid id, UpdateUserDto updateUserDto)
        {
            AppUser? oldUser = await _context.Users.FindAsync(id);
            if (oldUser == null) throw AppException.NotFound("User not found");
            _mapper.Map(updateUserDto, oldUser);
        }


    }
}