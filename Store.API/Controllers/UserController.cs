using Microsoft.AspNetCore.Mvc;
using Store.Application.DataTransferObjects;
using Store.Application.Services.Interfaces;

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
    }
}
