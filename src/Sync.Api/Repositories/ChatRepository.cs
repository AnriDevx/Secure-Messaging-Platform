using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sync.Api.Configurations;
using Sync.Api.Models;

namespace Sync.Api.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly IMongoCollection<ChatMessage> _messages;

        public ChatRepository(IMongoClient? client, IOptions<MongoDbConfig> config)
        {
            if (client == null || config == null || config.Value == null)
            {
                throw new ArgumentNullException("client or config is null");
            }
            var database = client.GetDatabase(config.Value.DatabaseName);
            _messages = database.GetCollection<ChatMessage>(config.Value.MessagesCollection);
        }

        public async Task<List<ChatMessage>> GetChatsAsync(string? userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }
            return await _messages.Find(m => m.SenderId == userId || m.ReceiverId == userId)
                .SortByDescending(m => m.Timestamp)
                .ToListAsync();
        }

        public async Task<List<ChatMessage>> GetMessagesAsync(string? chatId, string? userId)
        {
            if (string.IsNullOrEmpty(chatId) || string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("chatId or userId is null");
            }
            return await _messages.Find(m => (m.SenderId == userId && m.ReceiverId == chatId) || 
                                            (m.SenderId == chatId && m.ReceiverId == userId))
                .SortBy(m => m.Timestamp)
                .ToListAsync();
        }

        public async Task AddMessageAsync(ChatMessage? message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }
            await _messages.InsertOneAsync(message);
        }
    }
}