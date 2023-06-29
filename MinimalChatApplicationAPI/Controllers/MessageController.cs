using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalChatApplicationAPI.Dto;
using MinimalChatApplicationAPI.Service;
using MinimalChatApplicationAPI.CustomExceptions;

namespace MinimalChatApplicationAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : BaseController
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost("/messages")]
        public async Task<IActionResult> UpsertMessageAsync(MessageDto messageDto)
        {
            Guid senderId = GetUserId();
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
        public async Task<IActionResult> EditMessageAsync(string testMesage, string messageId = "{{messageId}}")
        {
            Guid senderId = GetUserId();
            try
            {
                var res = await _messageService.EditMessageAsync(messageId, testMesage, senderId);
                return Ok(res);
            }
            catch (UnauthorizedException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("/messages/{messageId}")]
        public async Task<IActionResult> DeleteMessageAsync(string messageId = "{{messageId}}")
        {
            Guid senderId = GetUserId();
            try
            {
                var res = await _messageService.DeleteMessageAsync(messageId, senderId);
                return Ok("Message deleted successfully");
            }
            catch (UnauthorizedException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/conversations/{userId}")]
        public async Task<IActionResult> GetConversationHistoryAsync([FromQuery] MessageHistoryDto messageHistoryDto, Guid userId)
        {
            Guid senderId = GetUserId();
            try
            {
                var res = await _messageService.GetConversationHistoryAsync(messageHistoryDto, senderId, userId);
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
