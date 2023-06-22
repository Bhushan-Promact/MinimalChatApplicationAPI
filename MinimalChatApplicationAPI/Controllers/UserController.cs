using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalChatApplicationAPI.CustomExceptions;
using MinimalChatApplicationAPI.Dto;
using MinimalChatApplicationAPI.Service;

namespace MinimalChatApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("/users"), Authorize]
        public async Task<IActionResult> GetUserListAsync()
        {
            Guid userID = GetUserId();
            try
            {
                var res = await _userService.GetUsersAsync(userID);
                return Ok(res);
            }
            catch (UnauthorizedException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/register")]
        public async Task<IActionResult> UpsertUserAsync(UserRegistrationDto userDto)
        {
            try
            {
                var res = await _userService.UpsertUser(userDto);
                return Ok(res);
            }
            catch (ConflictException ex)
            {
                return Conflict(ex);
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
