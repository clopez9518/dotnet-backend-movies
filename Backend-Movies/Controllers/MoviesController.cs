
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Extensions;
using Movies.Application.DTOs.Movie;
using Movies.Application.DTOs.Pagination;
using Movies.Application.Interfaces;

namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<MovieDto>>> GetAllMovies([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var profileId = User.GetProfileIdOrNull();
            var (movies, totalRecords, pageSizeR) = await _movieService.GetAll(page, pageSize, profileId);
            var response = new PagedResponse<MovieDto>
            {
                Data = movies,
                PageNumber = page,
                PageSize = pageSizeR,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSizeR)
            };
            return Ok(response);
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            var profileId = User.GetProfileIdOrNull();
            var movieDto = await _movieService.GetById(id, profileId);
            return Ok(movieDto);
        }

        [HttpGet("bygenre/{id}")]

        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMoviesByGenre(int id)
        {
            var profileId = User.GetProfileIdOrNull();
            var movies = await _movieService.GetByGenre(id, profileId);
            return Ok(movies);
        }

        [HttpGet("Trending")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetTrendingMovies()
        {
            var profileId = User.GetProfileIdOrNull();
            var movies = await _movieService.GetTrendingMovies(profileId);
            return Ok(movies);
        }

        [HttpGet("sort")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetSortMovies([FromQuery] string sortBy = "recent", [FromQuery] int limit = 15)
        {
            var profileId = User.GetProfileIdOrNull();
            var movies = await _movieService.GetSortMovies(sortBy, limit, profileId);
            return Ok(movies);
        }

        [HttpGet("Similar/{id}")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetSimilarMovies(int id)
        {
            var profileId = User.GetProfileIdOrNull();
            var movies = await _movieService.GetSimilarMovies(id, profileId);
            return Ok(movies);
        }

        [HttpGet("Hero")]
        public async Task<ActionResult<MovieDto>> GetHeroMovie([FromQuery] int? genreId)
        {
            var profileId = User.GetProfileIdOrNull();
            var movie = await _movieService.GetHeroMovie(genreId, profileId);
            return Ok(movie);
        }
    }
}
