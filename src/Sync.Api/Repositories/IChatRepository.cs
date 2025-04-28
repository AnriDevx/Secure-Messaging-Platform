using System.Collections.Generic;
using System.Threading.Tasks;
using Sync.Api.Models;

namespace Sync.Api.Repositories
{
    public interface IChatRepository
    {
        Task<List<ChatMessage>> GetChatsAsync(string? userId);
        Task<List<ChatMessage>> GetMessagesAsync(string? chatId, string? userId);
        Task AddMessageAsync(ChatMessage? message);
    }
}