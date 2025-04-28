using System.Threading.Tasks;
using Sync.Api.Models;

namespace Sync.Api.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(RegisterDto? registerDto);
        Task<string> GenerateJwtTokenAsync(LoginDto? loginDto);
    }
}