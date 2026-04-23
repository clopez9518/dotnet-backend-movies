

using AutoMapper;
using Movies.Application.DTOs.Movie;
using Movies.Application.DTOs.Profile;
using Movies.Application.DTOs.User;
using Movies.Application.DTOs.UserAdmin;
using Movies.Application.Exceptions;
using Movies.Application.Interfaces;
using Movies.Domain.Entities;

namespace Movies.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(
            IUserRepository userRepository, 
            IMapper mapper, 
            IPasswordHasher passwordHasher
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        
        public async Task<UserAdminDto> ChangeUserStatusAdmin(int userId, bool isActive)
        {
            var user = await _userRepository.GetUserById(userId) ??
                throw new NotFoundException("User not found", "USER_NOT_FOUND");
           
            if (!isActive)
            {
                foreach (var token in user.RefreshTokens)
                {
                    token.IsRevoked = true;
                }
            }
               
            user.IsActive = isActive;
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserAdminDto>(user);
        }

        public async Task<UserDto> CreateUser(CreateUserDto createUserDto)
        {
            if (!createUserDto.Password.Equals(createUserDto.PasswordConfirm)) 
                throw new BadRequestException("Passwords do not match", "PASSWORDS_DO_NOT_MATCH");


            var user = _mapper.Map<User>(createUserDto);

            user.CreatedAt = DateTime.UtcNow;
            user.PasswordHash = _passwordHasher.HashPassword(createUserDto.Password);

            user.Profiles = new List<Domain.Entities.Profile> { 
                new Domain.Entities.Profile
                {
                    AvatarUrl = "https://example.com/default-avatar.png",
                    IsKids=false,
                    Name = "Default"
                }
            };

            await _userRepository.Add(user);
            await _userRepository.SaveChangesAsync();

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email) ??
                throw new NotFoundException("User not found", "USER_NOT_FOUND");

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<UserDto> GetUserById(int userId)
        {
            var user = await _userRepository.GetUserById(userId) ??
                throw new NotFoundException("User not found", "USER_NOT_FOUND");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserAdminDto>> GetUsers()
        {
            var users = await _userRepository.GetUsersAdmin();
            return users.Select(u => _mapper.Map<UserAdminDto>(u));
        }
    }
}
