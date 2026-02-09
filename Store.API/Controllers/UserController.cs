using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.DataTransferObjects;
using Store.Application.Services.Interfaces;
using System.Security.Claims;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("user-info")]
        public async Task<IActionResult> GetUserInformation(CancellationToken cancellationToken)
        {
            var email = User?.FindFirstValue(ClaimTypes.Email);
            var role = User?.FindFirstValue(ClaimTypes.Role);

            if (email == null || role == null)
            {
                return Ok(null);
            }

            return Ok(new
            {
                email,
                role
            });
        }

        [HttpGet("email-valid")]
        public async Task<IActionResult> IsAnyUserByEmailAsync([FromQuery]string email, CancellationToken cancellationToken)
        {
            var result = await _userService.IsAnyUserByEmailAsync(email, cancellationToken);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDto userDto, CancellationToken cancellationToken)
        {
            var newUser = await _userService.RegisterUserAsync(userDto, cancellationToken);
            return Ok(newUser);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordDto forgotPasswordDto, CancellationToken cancellationToken)
        {
            await _userService.ForgotPasswordAsync(forgotPasswordDto, cancellationToken);
            return NoContent();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordDto resetPasswordDto, CancellationToken cancellationToken)
        {
            await _userService.ResetPasswordAsync(resetPasswordDto, cancellationToken);
            return NoContent();
        }

        [HttpPost("log-out")]
        public async Task<IActionResult> LogOutAsync(CancellationToken cancellationToken)
        {
            await _userService.LogOutAsync(cancellationToken);
            return NoContent();
        }

        [HttpGet("auth-status")]
        public async Task<IActionResult> GetAuthStatus(CancellationToken cancellationToken)
        {
            return Ok(new {
                isAuthenticated = User.Identity?.IsAuthenticated ?? false
            });
        }
    }
}
