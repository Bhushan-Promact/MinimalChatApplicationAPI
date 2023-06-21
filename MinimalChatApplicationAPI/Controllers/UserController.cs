using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalChatApplicationAPI.CustomExceptions;
using MinimalChatApplicationAPI.Dto;
using MinimalChatApplicationAPI.Service;
using MinimalChatApplicationAPI.Utils;
using MinimalChatApplicationAPI.Utils.Helpers;

namespace MinimalChatApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("/users"), Authorize]
        public async Task<IActionResult> GetUserListAsync([FromHeader] string authorization = "")
        {
            var userID = authorization.GetJwtClaimValueFromKey(JWTClaimTypes.Id.ToString()) ?? "";
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

        [HttpPost("/register"), Authorize]
        public async Task<IActionResult> UpsertUserAsync(UserRegistrationDto userDto)
        {
            //Guid userID = Guid.Parse(authorization.GetJwtClaimValueFromKey(JWTClaimTypes.Id.ToString()) ?? "");  ,[FromHeader] string authorization = ""
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
