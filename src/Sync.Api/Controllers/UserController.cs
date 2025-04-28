using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sync.Api.Models;
using Sync.Api.Services;
using System.Threading.Tasks;

namespace Sync.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService? userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirst("id")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            var user = await _userService.GetUserAsync(userId);
            return Ok(user);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteAccount()
        {
            var userId = User.FindFirst("id")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            await _userService.DeleteUserAsync(userId);
            return Ok();
        }
    }
}