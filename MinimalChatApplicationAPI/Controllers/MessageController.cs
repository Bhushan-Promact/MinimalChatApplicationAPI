using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalChatApplicationAPI.Dto;
using MinimalChatApplicationAPI.Utils.Helpers;
using MinimalChatApplicationAPI.Utils;
using MinimalChatApplicationAPI.Service;
using MinimalChatApplicationAPI.CustomExceptions;

namespace MinimalChatApplicationAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost("/messages")]
        public async Task<IActionResult> UpsertMessageAsync(MessageDto messageDto, [FromHeader] string authorization = "")
        {
            Guid senderId = Guid.Parse(authorization.GetJwtClaimValueFromKey(JWTClaimTypes.Id.ToString()) ?? "");
            try
            {
                var res = await _messageService.UpsertMessageAsync(messageDto, senderId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/messages/{messageId}")]
        public async Task<IActionResult> EditMessageAsync(string testMesage, string messageId = "{{messageId}}", [FromHeader] string authorization = "")
        {
            Guid senderId = Guid.Parse(authorization.GetJwtClaimValueFromKey(JWTClaimTypes.Id.ToString()) ?? "");
            try
            {
                var res = await _messageService.EditMessageAsync(messageId, testMesage, senderId);
                return Ok(res);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("/messages/{messageId}")]
        public async Task<IActionResult> DeleteMessageAsync(string messageId = "{{messageId}}", [FromHeader] string authorization = "")
        {
            Guid senderId = Guid.Parse(authorization.GetJwtClaimValueFromKey(JWTClaimTypes.Id.ToString()) ?? "");
            try
            {
                var res = await _messageService.DeleteMessageAsync(messageId, senderId);
                return Ok("Message deleted successfully");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/conversations/{userId}")]
        public async Task<IActionResult> GetConversationHistoryAsync([FromQuery] MessageHistoryDto messageHistoryDto, string userId, [FromHeader] string authorization = "")
        {
            Guid senderId = Guid.Parse(authorization.GetJwtClaimValueFromKey(JWTClaimTypes.Id.ToString()) ?? "");
            Guid receiverId = Guid.Parse(userId);
            try
            {
                var res = await _messageService.GetConversationHistoryAsync(messageHistoryDto, senderId, receiverId);
                return Ok(res);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BadDataException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
