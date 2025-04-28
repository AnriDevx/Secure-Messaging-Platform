using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Sync.Api.Controllers;
using Sync.Api.Models;
using Sync.Api.Services;
using System.Threading.Tasks;

namespace Sync.Api.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _controller = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public async Task Register_ValidDto_ReturnsOk()
        {
            var registerDto = new RegisterDto { Username = "test", Email = "test@example.com", Password = "password" };
            var user = new User { Username = "test" };
            _authServiceMock.Setup(s => s.RegisterAsync(It.IsAny<RegisterDto>())).ReturnsAsync(user);

            var result = await _controller.Register(registerDto);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Login_ValidDto_ReturnsOk()
        {
            var loginDto = new LoginDto { Email = "test@example.com", Password = "password" };
            _authServiceMock.Setup(s => s.GenerateJwtTokenAsync(It.IsAny<LoginDto>())).ReturnsAsync("jwt_token");

            var result = await _controller.Login(loginDto);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}