using System.Collections.Generic;
using System.Threading.Tasks;
using Sync.Api.Models;

namespace Sync.Api.Services
{
    public interface IChatService
    {
        Task<List<ChatMessage>> GetChatsAsync(string? userId);
        Task<List<ChatMessage>> GetMessagesAsync(string? chatId, string? userId);
        Task SendMessageAsync(string? chatId, string? userId, MessageDto? messageDto);
    }
}