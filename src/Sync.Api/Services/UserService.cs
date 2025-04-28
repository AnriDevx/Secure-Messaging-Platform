using System.Threading.Tasks;
using Sync.Api.Models;
using Sync.Api.Repositories;

namespace Sync.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository? userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<UserDto> GetUserAsync(string? userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return new UserDto
            {
                Id = user.Id.ToString(),
                Username = user.Username,
                Email = user.Email
            };
        }

        public async Task DeleteUserAsync(string? userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }
            await _userRepository.DeleteAsync(userId);
        }
    }
}