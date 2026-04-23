

using AutoMapper;
using Movies.Application.DTOs.Movie;
using Movies.Application.Exceptions;
using Movies.Application.Interfaces;
using Movies.Domain.Entities;

namespace Movies.Application.Services
{
    public class MovieService : IMovieService
    {

        private readonly IMovieRepository _movieRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        public MovieService(
            IMovieRepository movieRepository,
            IProfileRepository profileRepository,
            IMapper mapper
            )
        {
            _movieRepository = movieRepository;
            _profileRepository = profileRepository;
            _mapper = mapper;
        }


        public async Task<(IEnumerable<MovieDto> Movies, int TotalRecords, int PageSize)> GetAll(int page = 1, int pageSize = 10, int? profileId = null)
        {

            const int maxPageSize = 50;
            const int defaultPageSize = 10;

            pageSize = pageSize <= 0 ? defaultPageSize : pageSize;
            pageSize = pageSize > maxPageSize ? maxPageSize : pageSize;

            var movies = await _movieRepository.GetAllAsync(page, pageSize);
            var movieDtos = movies.Select(movie => _mapper.Map<MovieDto>(movie)).ToList();

            var totalRecords = await _movieRepository.TotalMoviesCountAsync();

            if (profileId.HasValue)
            {
                await SetIsInMyList(profileId.Value, movieDtos);
            }
            return (movieDtos, totalRecords, pageSize);
        }

        public async Task<IEnumerable<MovieDto>> GetByGenre(int genreId, int? profileId)
        {
            var movies = await _movieRepository.GetMoviesByGenre(genreId);
            var movieDtos = movies.Select(movie => _mapper.Map<MovieDto>(movie)).ToList();
            if (profileId.HasValue)
            {
                await SetIsInMyList(profileId.Value, movieDtos);
            }
            return movieDtos;
        }

        public async Task<MovieDto> GetById(int id, int? profileId)
        {
            var movie = await _movieRepository.GetByIdAsync(id) ?? 
                throw new NotFoundException("Movie not found", "MOVIE_NOT_FOUND");

            var movieDto = _mapper.Map<MovieDto>(movie);
            if (profileId.HasValue)
            {
                await SetIsInMyList(profileId.Value, new List<MovieDto> { movieDto });
            }
            return movieDto;
        }

        public async Task<MovieDto> GetHeroMovie(int? genreId, int? profileId)
        {
            IEnumerable<Movie> movies;

            if (genreId.HasValue)
            {
                movies = await _movieRepository.GetMoviesByGenre(genreId.Value);
            }
            else
            {
                movies = await _movieRepository.GetAllAsync();
            }

            if (!movies.Any())
                throw new NotFoundException("No movies found", "MOVIE_NOT_FOUND");

            var now = DateTime.UtcNow;

            var scoredMovies = movies.Select(m => new
            {
                Movie = m,
                Score =
                GetRecencyScore(m.CreatedAt, now) * 0.5 +
                (m.IsTrending ? 1 : 0) * 0.3 +
                (m.Rating / 10.0) * 0.2
            });

            var movie = scoredMovies
                .OrderByDescending(x => x.Score)
                .Take(1)
                .Select(x => x.Movie)
                .FirstOrDefault();

            //var random = new Random();
            //var selected = topMovies[random.Next(topMovies.Count)];

            var movieDto = _mapper.Map<MovieDto>(movie);

            if (profileId.HasValue)
            {
                await SetIsInMyList(profileId.Value, new List<MovieDto> { movieDto });
            }
            return movieDto;
        }

        public async Task<IEnumerable<MovieDto>> GetSimilarMovies(int movieId, int? profileId)
        {
            var movies = await _movieRepository.GetSimilarMovies(movieId);
            var movieDtos = movies.Select(movie => _mapper.Map<MovieDto>(movie)).ToList();
            if (profileId.HasValue)
            {
                await SetIsInMyList(profileId.Value, movieDtos);
            }
            return movieDtos;
        }

        public async Task<IEnumerable<MovieDto>> GetTrendingMovies(int? profileId)
        {
            var movies = await _movieRepository.GetTrendingMovies();
            var movieDtos = movies.Select(movie => _mapper.Map<MovieDto>(movie)).ToList();
            if (profileId.HasValue)
            {
                await SetIsInMyList(profileId.Value, movieDtos);
            }
            return movieDtos;
        }


        public async Task<IEnumerable<MovieDto>> GetSortMovies(string sortBy = "recent", int limit = 15, int? profileId = null)
        {
            var movies = await _movieRepository.GetMoviesSortAsync(sortBy, limit);
            var movieDtos = movies.Select(movie => _mapper.Map<MovieDto>(movie)).ToList();
            if (profileId.HasValue)
            {
                await SetIsInMyList(profileId.Value, movieDtos);
            }
            return movieDtos;
        }

        private async Task<IEnumerable<MovieDto>> SetIsInMyList(int profileId, List<MovieDto> movies) 
        {
            var myListMovieIds = await _profileRepository.GetMyListMoviesIds(profileId);
            foreach (var movie in movies)
            {
                movie.IsInMyList = myListMovieIds.Contains(movie.Id);
            }
            return movies;
        }

        private double GetRecencyScore(DateTime createdAt, DateTime now)
        {
            var days = (now - createdAt).TotalDays;

            if (days <= 7) return 1;
            if (days <= 30) return 0.7;
            if (days <= 90) return 0.4;

            return 0.1;
        }

    }
}
