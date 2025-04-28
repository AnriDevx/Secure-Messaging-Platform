using Moq;
using Xunit;
using Sync.Api.Models;
using Sync.Api.Repositories;
using Sync.Api.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sync.Api.Tests.Services
{
    public class ChatServiceTests
    {
        private readonly Mock<IChatRepository> _chatRepositoryMock;
        private readonly ChatService _chatService;

        public ChatServiceTests()
        {
            _chatRepositoryMock = new Mock<IChatRepository>();
            _chatService = new ChatService(_chatRepositoryMock.Object);
        }

        [Fact]
        public async Task GetChats_ValidUserId_ReturnsChats()
        {
            var chats = new List<ChatMessage> { new ChatMessage { SenderId = "user1" } };
            _chatRepositoryMock.Setup(r => r.GetChatsAsync(It.IsAny<string>())).ReturnsAsync(chats);

            var result = await _chatService.GetChatsAsync("user1");

            Assert.NotEmpty(result);
            Assert.Equal("user1", result[0].SenderId);
        }

        [Fact]
        public async Task GetMessages_ValidChatId_ReturnsMessages()
        {
            var messages = new List<ChatMessage> { new ChatMessage { SenderId = "user1", ReceiverId = "user2" } };
            _chatRepositoryMock.Setup(r => r.GetMessagesAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(messages);

            var result = await _chatService.GetMessagesAsync("user2", "user1");

            Assert.NotEmpty(result);
            Assert.Equal("user2", result[0].ReceiverId);
        }

        [Fact]
        public async Task SendMessage_ValidData_CallsRepository()
        {
            _chatRepositoryMock.Setup(r => r.AddMessageAsync(It.IsAny<ChatMessage>())).Returns(Task.CompletedTask);

            await _chatService.SendMessageAsync("user2", "user1", new MessageDto { Content = "Hello" });

            _chatRepositoryMock.Verify(r => r.AddMessageAsync(It.IsAny<ChatMessage>()), Times.Once());
        }
    }
}