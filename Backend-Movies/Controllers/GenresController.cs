using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.DTOs.Genre;
using Movies.Application.Interfaces;

namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {

        private IGenreService _genreService;

        public GenresController(IGenreService genreService) { 
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var genres = await _genreService.GetAll();
            return genres == null ? NotFound() : Ok(genres);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var genre = await _genreService.GetById(id);
            return genre == null ? NotFound() : Ok(genre);
        }

        [HttpPost]
        
        public async Task<ActionResult<GenreDto>> PostGenre(CreateGenreDto dto)
        {
            var genre = await _genreService.Create(dto);
            return genre == null ? NotFound() : Ok(genre);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GenreDto>> PutGenre(int id,UpdateGenreDto dto)
        {
            var genre = await _genreService.Update(id, dto);
            return genre == null ? NotFound() : Ok(genre);
        }
    }
}
