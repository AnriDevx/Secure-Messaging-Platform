using System.Collections.Generic;
using System.Threading.Tasks;
using Sync.Api.Models;
using Sync.Api.Repositories;

namespace Sync.Api.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository? chatRepository)
        {
            _chatRepository = chatRepository ?? throw new ArgumentNullException(nameof(chatRepository));
        }

        public async Task<List<ChatMessage>> GetChatsAsync(string? userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }
            return await _chatRepository.GetChatsAsync(userId);
        }

        public async Task<List<ChatMessage>> GetMessagesAsync(string? chatId, string? userId)
        {
            if (string.IsNullOrEmpty(chatId) || string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("chatId or userId is null");
            }
            return await _chatRepository.GetMessagesAsync(chatId, userId);
        }

        public async Task SendMessageAsync(string? chatId, string? userId, MessageDto? messageDto)
        {
            if (string.IsNullOrEmpty(chatId) || string.IsNullOrEmpty(userId) || messageDto == null)
            {
                throw new ArgumentNullException("chatId, userId, or messageDto is null");
            }
            var message = new ChatMessage
            {
                SenderId = userId,
                ReceiverId = chatId,
                Content = messageDto.Content,
                Timestamp = DateTime.UtcNow,
                IsRead = false
            };
            await _chatRepository.AddMessageAsync(message);
        }
    }
}