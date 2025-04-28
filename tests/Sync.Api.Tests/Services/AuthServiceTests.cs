using Moq;
using Xunit;
using Sync.Api.Models;
using Sync.Api.Repositories;
using Sync.Api.Services;
using System.Threading.Tasks;

namespace Sync.Api.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(c => c["Jwt:Secret"]).Returns("your-very-secure-secret-key-here");
            _authService = new AuthService(_userRepositoryMock.Object, _configurationMock.Object);
        }

        [Fact]
        public async Task Register_ValidDto_ReturnsUser()
        {
            var registerDto = new RegisterDto { Username = "test", Email = "test@example.com", Password = "password" };
            var user = new User { Username = "test" };
            _userRepositoryMock.Setup(r => r.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

            var result = await _authService.RegisterAsync(registerDto);

            Assert.NotNull(result);
            Assert.Equal("test", result.Username);
        }
    }
}