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
    }
}
