using Microsoft.AspNetCore.Mvc;
using Sync.Api.Models;
using Sync.Api.Services;
using System.Threading.Tasks;

namespace Sync.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService? authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto? registerDto)
        {
            if (registerDto == null)
            {
                return BadRequest("RegisterDto is null");
            }
            var result = await _authService.RegisterAsync(registerDto);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto? loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest("LoginDto is null");
            }
            var token = await _authService.GenerateJwtTokenAsync(loginDto);
            return Ok(new { Token = token });
        }
    }
}