using System.Threading.Tasks;
using Sync.Api.Models;

namespace Sync.Api.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUserAsync(string? userId);
        Task DeleteUserAsync(string? userId);
    }
}