using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sync.Api.Models;
using Sync.Api.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sync.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService? chatService)
        {
            _chatService = chatService ?? throw new ArgumentNullException(nameof(chatService));
        }

        [HttpGet]
        public async Task<IActionResult> GetChats()
        {
            var userId = User.FindFirst("id")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            var chats = await _chatService.GetChatsAsync(userId);
            return Ok(chats);
        }

        [HttpGet("{chatId}")]
        public async Task<IActionResult> GetMessages(string? chatId)
        {
            if (string.IsNullOrEmpty(chatId))
            {
                return BadRequest("ChatId is required");
            }
            var userId = User.FindFirst("id")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            var messages = await _chatService.GetMessagesAsync(chatId, userId);
            return Ok(messages);
        }

        [HttpPost("{chatId}")]
        public async Task<IActionResult> SendMessage(string? chatId, [FromBody] MessageDto? messageDto)
        {
            if (string.IsNullOrEmpty(chatId) || messageDto == null)
            {
                return BadRequest("ChatId or MessageDto is null");
            }
            var userId = User.FindFirst("id")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            await _chatService.SendMessageAsync(chatId, userId, messageDto);
            return Ok();
        }
    }
}