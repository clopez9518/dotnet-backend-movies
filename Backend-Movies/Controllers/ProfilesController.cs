using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Extensions;
using Movies.Application.DTOs.Movie;
using Movies.Application.DTOs.Profile;
using Movies.Application.Interfaces;

namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProfilesController : ControllerBase
    {

        private readonly IProfileService _profileService;
        public ProfilesController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPost]
        public async Task<ActionResult<ProfileDto>> AddProfile(CreateProfileDto createProfileDto)
        {
            var userId = User.GetUserId();
            var profile = await _profileService.Add(createProfileDto, userId);
            return Ok(profile);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfileDto>>> GetProfiles()
        {
            var userId = User.GetUserId();
            var profiles = await _profileService.GetAllUserProfiles(userId);
            return Ok(profiles);
        }

        [HttpGet("mylist/{id}")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMyList(int id)
        {
            var userId = User.GetUserId();
            var movies = await _profileService.GetMyList(id, userId);
            return Ok(movies);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var userId = User.GetUserId();
            await _profileService.Delete(id, userId);
            return NoContent();
        }

        [HttpPost("mylist/{movieId}")]
        public async Task<ActionResult<MovieDto>> AddToMyList(int movieId, [FromBody] AddToMyListDto dto)
        {
            var userId = User.GetUserId();
            var movie = await _profileService.AddToMyList(dto.ProfileId, movieId, userId);
            return Ok(movie);
        }

        [HttpDelete("mylist/{movieId}")]
        public async Task<ActionResult<MovieDto>> RemoveFromMyList(int movieId, [FromBody] AddToMyListDto dto)
        {
            var userId = User.GetUserId();
            var movie = await _profileService.RemoveFromMyList(dto.ProfileId, movieId, userId);
            return Ok(movie);
        }
    }
}
