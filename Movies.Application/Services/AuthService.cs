using AutoMapper;
using Movies.Application.DTOs.Auth;
using Movies.Application.DTOs.User;
using Movies.Application.Exceptions;
using Movies.Application.Interfaces;
using Movies.Domain.Entities;

namespace Movies.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IProfileService _profileService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public AuthService(
            IUserRepository userRepository,
            IJwtService jwtService,
            IProfileService profileService,
            IPasswordHasher passwordHasher,
            IMapper mapper
            )
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _profileService = profileService;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<AuthResponseDto?> Login(LoginDto dto)
        {
            var user = await _userRepository.GetUserByEmail(dto.Email);

            if (user == null || !_passwordHasher.VerifyPassword(dto.Password, user.PasswordHash)) 
                throw new UnauthorizedException("Invalid Credentials", "INVALID_CREDENTIALS");

            var accessToken = _jwtService.GenerateJwt(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            user.LastLoginAt = DateTime.UtcNow;
            user.RefreshTokens.Add(refreshToken);
            await _userRepository.SaveChangesAsync();

            return new AuthResponseDto()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = refreshToken.Expires,
                User = _mapper.Map<UserDto>(user)
            };
        }

        public async Task Logout(string refreshToken)
        {
            var user = await _userRepository.GetByRefreshToken(refreshToken);
            if (user == null) return;

            var token = user.RefreshTokens.First(rt => rt.Token == refreshToken);

            token.IsRevoked = true;

            await _userRepository.SaveChangesAsync();
        }

        public async Task<AuthResponseDto> Refresh(string token)
        {
            var user = await _userRepository.GetByRefreshToken(token) ?? 
                throw new UnauthorizedException("Invalid refresh token", "INVALID_REFRESH_TOKEN");

            var refreshToken = user.RefreshTokens.First(rt => rt.Token == token);
            var profileId = refreshToken.ProfileId;

            if (refreshToken.IsRevoked || refreshToken.Expires < DateTime.UtcNow) 
                throw new UnauthorizedException("Invalid refresh token", "INVALID_REFRESH_TOKEN");

            refreshToken.IsRevoked = true;

            var newRefreshToken = _jwtService.GenerateRefreshToken();
            newRefreshToken.ProfileId = profileId;
            user.RefreshTokens.Add(newRefreshToken);

            var newAccessToken = _jwtService.GenerateJwt(user, profileId);
            await _userRepository.SaveChangesAsync();

            return new AuthResponseDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Token,
                ExpiresAt = newRefreshToken.Expires,
                User = _mapper.Map<UserDto>(user)
            };
        }

        public async Task<AuthResponseDto> Register(RegisterDto dto)
        {

            if (!dto.Password.Equals(dto.PasswordConfirm))
                throw new BadRequestException("Passwords do not match", "PASSWORDS_DO_NOT_MATCH");
            
            var existingUser = await _userRepository.GetUserByEmail(dto.Email);
            if (existingUser != null) 
                throw new BadRequestException("The request could not be processed", "BAD_REQUEST");

            var user = _mapper.Map<User>(dto);

            user.CreatedAt = DateTime.UtcNow;
            user.PasswordHash = _passwordHasher.HashPassword(dto.Password);
            user.Role = "user";
            user.Profiles.Add(new Domain.Entities.Profile() 
            { 
                Name = "Default Profile",
                AvatarUrl = "https://example.com/default-avatar.png",
                IsKids = false
            });

            // Guardar al usuario antes de generar el token para obtener su ID
            await _userRepository.Add(user);
            await _userRepository.SaveChangesAsync();

            var accessToken = _jwtService.GenerateJwt(user);
            var refreshToken = _jwtService.GenerateRefreshToken();
            user.RefreshTokens.Add(refreshToken);

            await _userRepository.SaveChangesAsync();
            return new AuthResponseDto()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = refreshToken.Expires,
                User = _mapper.Map<UserDto>(user)
            };
        }

        public async Task<AuthResponseDto?> SelectProfile(int userId, int profileId)
        {
            var user = await _userRepository.GetUserById(userId) ?? 
                throw new NotFoundException("User not found", "USER_NOT_FOUND");

            if (!await _profileService.UserOwnsProfile(userId, profileId)) 
                throw new NotFoundException("Profile not found", "PROFILE_NOT_FOUND");

            var accessToken = _jwtService.GenerateJwt(user, profileId);
            var refreshToken = _jwtService.GenerateRefreshToken();
            refreshToken.ProfileId = profileId;

            user.RefreshTokens.Add(refreshToken);

            await _userRepository.SaveChangesAsync();

            return new AuthResponseDto()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = refreshToken.Expires,
                User = _mapper.Map<UserDto>(user)
            };

        }
    }
}
