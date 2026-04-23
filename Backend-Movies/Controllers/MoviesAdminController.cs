using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.DTOs.Movie;
using Movies.Application.DTOs.MovieAdmin;
using Movies.Application.Interfaces;

namespace Movies.Api.Controllers
{
    [Route("api/admin/movies")]
    [Authorize(Roles = "admin")]
    [ApiController]
    public class MoviesAdminController : ControllerBase
    {

        private IMovieAdminService _movieAdminService;

        public MoviesAdminController(IMovieAdminService movieService)
        {
            _movieAdminService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var movieDto = await _movieAdminService.GetAll();
            return movieDto == null ? NotFound() : Ok(movieDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movieDto = await _movieAdminService.GetById(id);
            return movieDto == null ? NotFound() : Ok(movieDto);
        }


        [HttpPost]
        public async Task<ActionResult<MovieAdminDto>> PostMovie(CreateMovieAdminDto createMovieDto)
        {
            //Add validators
            var movieDto = await _movieAdminService.Create(createMovieDto);
            return movieDto == null ? NotFound() : Ok(movieDto);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<MovieAdminDto>> PatchMovie(int id, UpdateMovieAdminDto updateMovieDto)
        {
            //Add validators
            var movieDto = await _movieAdminService.Update(id, updateMovieDto);
            return movieDto == null ? NotFound() : Ok(movieDto);
        }

        [HttpPut("genres/{id}")]
        public async Task<ActionResult<MovieAdminDto>> PutMovieGenres(int id, UpdateMovieGenresDto dto)
        {
            //Add validators
            var movieDto = await _movieAdminService.UpdateMovieGenres(id, dto.GenreIds);
            return movieDto == null ? NotFound() : Ok(movieDto);
        }

        [HttpGet("tmdb")]
        public async Task<ActionResult> GetTmdbMovies([FromQuery] string search)
        {
            var movies = await _movieAdminService.GetTmdbMoviesBySearch(search);
            return movies == null ? NotFound() : Ok(movies);
        }

        [HttpGet("tmdb/{id}")]
        public async Task<ActionResult> GetTmdbMovieById(int id)
        {
            var movie = await _movieAdminService.GetTmdbMovieById(id);
            return movie == null ? NotFound() : Ok(movie);
        }

    }
}
