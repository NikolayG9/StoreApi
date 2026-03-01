using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.DataTransferObjects;
using Store.Application.Services.Interfaces;
using Store.Domain.Constants;
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

        [HttpGet("get-all")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            var result = await _userService.GetAllUsersAsync(cancellationToken);
            return Ok(result);
        }

        [HttpGet("user-info")]
        public async Task<IActionResult> GetUserInformation(CancellationToken cancellationToken)
        {
            var id = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = User?.FindFirstValue(ClaimTypes.Email);
            var role = User?.FindFirstValue(ClaimTypes.Role);

            if (id == null || email == null || role == null)
            {
                return Ok(null);
            }

            return Ok(new
            {
                id,
                email,
                role
            });
        }

        [HttpGet("general-user-information")]
        public async Task<IActionResult> GetGeneralUserInformation(string id, CancellationToken cancellationToken)
        {
            var result = await _userService.GetUserInformationByIdAsync(id, cancellationToken);
            return Ok(result);
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

        [AllowAnonymous]
        [HttpGet("auth-status")]
        public async Task<IActionResult> GetAuthStatus(CancellationToken cancellationToken)
        {
            var isAuthenticated = User?.Identity?.IsAuthenticated ?? false;
            return Ok(new {isAuthenticated});
        }
    }
}
