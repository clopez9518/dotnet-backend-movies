

using AutoMapper;
using Movies.Application.DTOs.Movie;
using Movies.Application.DTOs.Profile;
using Movies.Application.DTOs.User;
using Movies.Application.Interfaces;
using Movies.Application.Exceptions;
using Movies.Domain.Entities;

namespace Movies.Application.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IUserService _userService;
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;
        public ProfileService(
            IUserService userService, 
            IProfileRepository profileRepository, 
            IMovieService movieService, 
            IMapper mapper
            )
        {
            _userService = userService;
            _profileRepository = profileRepository;
            _movieService = movieService;
            _mapper = mapper;
        }

        public async Task<ProfileDto> Add(CreateProfileDto dto, int userId)
        {
            var user = await _userService.GetUserById(userId)
                ?? throw new UnauthorizedException("User not found", "USER_NOT_FOUND");

            if (user.Profiles.Count >= 5) 
                throw new BadRequestException("Maximum limit of profiles reached", "MAXIMUM_PROFILES_REACHED");

            var profile = _mapper.Map<Domain.Entities.Profile>(dto);
            profile.AvatarUrl = "https://example.com/default-avatar.png";
            profile.UserId = user.Id;

            await _profileRepository.Add(profile);
            await _profileRepository.SaveChangesAsync();

            return _mapper.Map<ProfileDto>(profile);
        }

        public async Task<MovieDto> AddToMyList(int profileId, int movieId, int userId)
        {
            var profile = await _profileRepository.GetByIdAndUserId(profileId, userId) ??
                throw new NotFoundException("Profile not found", "PROFILE_NOT_FOUND");

            var movie = await _movieService.GetById(movieId, profileId) ?? 
                throw new NotFoundException("Movie not found", "MOVIE_NOT_FOUND");

            var exists = await _profileRepository.ExistsInMyList(profileId, movieId);

            if (exists) 
               throw new BadRequestException("Movie already in My List", "MOVIE_ALREADY_IN_MY_LIST");
            

            await _profileRepository.AddToMyList(profile, movieId);
            await _profileRepository.SaveChangesAsync();

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<ProfileDto> CreateDefault(UserDto user)
        {
            var profile = new Domain.Entities.Profile
            {
                Name = "Default",
                AvatarUrl = "https://example.com/default-avatar.png",
                IsKids = false,
                UserId = user.Id
            };

            await _profileRepository.Add(profile);
            await _profileRepository.SaveChangesAsync();

            return _mapper.Map<ProfileDto>(profile);
        }

        public async Task Delete(int profileId, int userId)
        {
            var profile = await _profileRepository.GetByIdAndUserId(profileId, userId) ?? 
                throw new NotFoundException("Profile not found", "PROFILE_NOT_FOUND");

            await _profileRepository.Delete(profileId);
            await _profileRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProfileDto>> GetAllUserProfiles(int userId)
        {
            var profiles = await _profileRepository.GetAllUserProfiles(userId) ?? 
                throw new NotFoundException("No profiles found for this user", "PROFILES_NOT_FOUND");
            return profiles.Select(p => _mapper.Map<ProfileDto>(p));
        }

        public async Task<ProfileDto> GetById(int profileId)
        {
            var profile = await _profileRepository.GetById(profileId) ??
                throw new NotFoundException("Profile not found", "PROFILE_NOT_FOUND");
            return _mapper.Map<ProfileDto>(profile);
        }

        public async Task<IEnumerable<MovieDto>> GetMyList(int profileId, int userId)
        {
            var profile = await _profileRepository.GetByIdAndUserId(profileId, userId) ??
                throw new NotFoundException("Profile not found", "PROFILE_NOT_FOUND");

            var movies = await _profileRepository.GetMyList(profileId);
            return movies.Select(m => { var dto = _mapper.Map<MovieDto>(m); dto.IsInMyList = true; return dto; });
        }

        public async Task<IEnumerable<int>> GetMyListMoviesIds(int profileId)
        {
            var moviesIds = await _profileRepository.GetMyListMoviesIds(profileId);
            return moviesIds;
        }

        public async Task<MovieDto> RemoveFromMyList(int profileId, int movieId, int userId)
        {
            var profile = await _profileRepository.GetByIdAndUserId(profileId, userId) ??
                throw new NotFoundException("Profile not found", "PROFILE_NOT_FOUND");

            var movie = await _movieService.GetById(movieId, profileId) ??
                throw new NotFoundException("Movie not found", "MOVIE_NOT_FOUND");

            var exists = await _profileRepository.ExistsInMyList(profileId, movieId);

            if (!exists)
                throw new BadRequestException("Movie not in My List", "MOVIE_NOT_IN_MY_LIST");


            await _profileRepository.RemoveFromMyList(profileId, movieId);
            await _profileRepository.SaveChangesAsync();

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<bool> UserOwnsProfile(int userId, int profileId)
        {
            var profile = await _profileRepository.GetByIdAndUserId(profileId, userId);
            return profile != null;
        }
    }
}
