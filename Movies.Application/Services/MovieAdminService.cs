using AutoMapper;
using Movies.Application.DTOs.MovieAdmin;
using Movies.Application.DTOs.TMDb;
using Movies.Application.Exceptions;
using Movies.Application.Extensions;
using Movies.Application.Interfaces;
using Movies.Domain.Entities;


namespace Movies.Application.Services
{
    public class MovieAdminService : IMovieAdminService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        private readonly ITmdbService _tmdbService;
        public MovieAdminService(
            IMovieRepository movieRepository,
            IMapper mapper,
            ITmdbService tmdbService
            )
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
            _tmdbService = tmdbService;
 
        }

        public async Task<IEnumerable<MovieAdminDto>> GetAll()
        {
            var movies = await _movieRepository.GetAllAsyncAdmin();
            return movies.Select(movie => _mapper.Map<MovieAdminDto>(movie));
        }

        public async Task<MovieAdminDto> GetById(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id) ?? 
                throw new NotFoundException("Movie not found", "MOVIE_NOT_FOUND");
         
            return _mapper.Map<MovieAdminDto>(movie);
        }

        public async Task<MovieAdminDto> Create(CreateMovieAdminDto createMovieDto)
        {
            var movie = _mapper.Map<Movie>(createMovieDto);
            movie.CreatedAt = DateTime.UtcNow;

            await _movieRepository.AddAsync(movie);
            await _movieRepository.SaveChangesAsync();

            //await _movieRepository.LoadGenre(movie);

            return _mapper.Map<MovieAdminDto>(movie);
        }

        public async Task<MovieAdminDto> Update(int id, UpdateMovieAdminDto updateMovieDto)
        {
            var movie = await _movieRepository.GetByIdAsync(id) ?? 
                throw new NotFoundException("Movie not found", "MOVIE_NOT_FOUND");

            MovieExtensions.ApplyPatch(movie, updateMovieDto);
            movie.UpdatedAt = DateTime.UtcNow;

            await _movieRepository.SaveChangesAsync();

            var movieDto = _mapper.Map<MovieAdminDto>(movie);
            return movieDto;

        }

        public async Task<MovieAdminDto> UpdateMovieGenres(int movieId, List<int> genreIds)
        {
            var movie = await _movieRepository.GetByIdAsync(movieId) ??
                throw new NotFoundException("Movie not found", "MOVIE_NOT_FOUND");


            //Update MovieGenres
            _movieRepository.UpdateMovieGenres(movie, genreIds);

            movie.UpdatedAt = DateTime.UtcNow;

            await _movieRepository.SaveChangesAsync();

            movie = await _movieRepository.GetByIdAsync(movieId);

            var movieDto = _mapper.Map<MovieAdminDto>(movie);
            return movieDto;

        }

        public async Task<IEnumerable<TmdbMovieDto>> GetTmdbMoviesBySearch(string search)
        {
            var movies = await _tmdbService.GetMoviesBySearch(search);
            return movies;
        }

        public async Task<TmdbMovieResponseDto> GetTmdbMovieById(int movieId)
        {
            var movie = await _tmdbService.GetMovieDetailsAsync(movieId) ??
                throw new NotFoundException("Movie not found", "MOVIE_NOT_FOUND");

            movie.Credits.Cast = movie.Credits.Cast.Take(5).ToList();
            movie.Credits.Crew = movie.Credits.Crew.Take(5).ToList();

            return movie;
        }
    }
}
