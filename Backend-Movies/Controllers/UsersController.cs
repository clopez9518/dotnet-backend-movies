using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.DTOs.User;
using Movies.Application.DTOs.UserAdmin;
using Movies.Application.Interfaces;


namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<UserDto>> GetUserByEmail(string email) { 
            var user = await _userService.GetUserByEmail(email);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(CreateUserDto createUserDto) {
            var user = await _userService.CreateUser(createUserDto);
            return user == null ? NotFound() : Ok(user);
        }

        
        [Authorize(Roles = "admin")]
        [HttpGet("admin")]
        public async Task<ActionResult<IEnumerable<UserAdminDto>>> GetUsersAdmin()
        {
            var users = await _userService.GetUsers();
            return users == null ? NotFound() : Ok(users);
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("{id}/change-status")]
        public async Task<ActionResult<UserAdminDto>> ChangeUserStatusAdmin(int id, [FromBody] ChangeUserStatusAdminDto dto)
        {
            var user = await _userService.ChangeUserStatusAdmin(id, dto.isActive);
            return user == null ? NotFound() : Ok(user);
        }
    }
}
