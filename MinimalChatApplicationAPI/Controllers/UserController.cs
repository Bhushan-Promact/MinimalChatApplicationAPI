using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalChatApplicationAPI.CustomExceptions;
using MinimalChatApplicationAPI.Dto;
using MinimalChatApplicationAPI.Service;

namespace MinimalChatApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpGet("/userList"), Authorize]
        public async Task<IActionResult> GetUserListAsync()
        {
            try
            {
                var res = await _userService.GetUsersAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/addUser"), Authorize]
        public async Task<IActionResult> UpsertUserAsync(UserRegistrationDto userDto)
        {
            try
            {
                var res = await _userService.UpsertUser(userDto);
                return Ok(res);
            }
            catch (ConflictException ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("/login")]
        public async Task<IActionResult> LoginUserAsync(UserLoginDto userLoginDto)
        {
            try
            {
                var res = await _userService.LoginUserAsync(userLoginDto);
                return Ok(res);
            }
            catch (UnauthorizedException ex)
            {
                return Unauthorized(ex);
            }
            catch (BadDataException ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
