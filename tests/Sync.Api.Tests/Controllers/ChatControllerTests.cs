using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Sync.Api.Controllers;
using Sync.Api.Models;
using Sync.Api.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sync.Api.Tests.Controllers
{
    public class ChatControllerTests
    {
        private readonly Mock<IChatService> _chatServiceMock;
        private readonly ChatController _controller;

        public ChatControllerTests()
        {
            _chatServiceMock = new Mock<IChatService>();
            _controller = new ChatController(_chatServiceMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("id", "user1") }))
                    }
                }
            };
        }

        [Fact]
        public async Task GetChats_ValidUser_ReturnsOk()
        {
            var chats = new List<ChatMessage> { new ChatMessage { SenderId = "user1" } };
            _chatServiceMock.Setup(s => s.GetChatsAsync(It.IsAny<string>())).ReturnsAsync(chats);

            var result = await _controller.GetChats();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetMessages_ValidChatId_ReturnsOk()
        {
            var messages = new List<ChatMessage> { new ChatMessage { SenderId = "user1", ReceiverId = "user2" } };
            _chatServiceMock.Setup(s => s.GetMessagesAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(messages);

            var result = await _controller.GetMessages("user2");

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SendMessage_ValidData_ReturnsOk()
        {
            _chatServiceMock.Setup(s => s.SendMessageAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageDto>())).Returns(Task.CompletedTask);

            var result = await _controller.SendMessage("user2", new MessageDto { Content = "Hello" });

            Assert.IsType<OkResult>(result);
        }
    }
}