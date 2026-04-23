using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Extensions;
using Movies.Application.DTOs.Auth;
using Movies.Application.Interfaces;
using Movies.Infrastructure.Cookies;

namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ICookieService _cookieService;

        public AuthController(IAuthService authService, ICookieService cookieService)
        {
            _authService = authService;
            _cookieService = cookieService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
        {
            var authResponse = await _authService.Login(loginDto);
            if (authResponse == null) return Unauthorized();

            _cookieService.SetRefreshToken(Response, authResponse.RefreshToken);

            return Ok(authResponse);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (refreshToken == null) return Unauthorized();

            await _authService.Logout(refreshToken);
            _cookieService.DeleteRefreshToken(Response);
            return Ok();
        }

        [HttpPost("refresh")]
        public async Task<ActionResult> Refresh()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (refreshToken == null) return Unauthorized();

            var authResponse = await _authService.Refresh(refreshToken);
            if (authResponse == null) return Unauthorized();

            _cookieService.SetRefreshToken(Response, authResponse.RefreshToken);

            return Ok(authResponse);

        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto registerDto)
        {
            var authResponse = await _authService.Register(registerDto);
            if (authResponse == null) return BadRequest("User with this email already exists.");
            _cookieService.SetRefreshToken(Response, authResponse.RefreshToken);
            return Ok(authResponse);
        }

        [Authorize]
        [HttpPost("select-profile/{profileId}")]
        public async Task<ActionResult<AuthResponseDto>> SelectProfile(int profileId)
        {
            var userId = User.GetUserId();
            var authResponse = await _authService.SelectProfile(userId, profileId);
            _cookieService.SetRefreshToken(Response, authResponse.RefreshToken);
            return Ok(authResponse);
        }
    }
}
