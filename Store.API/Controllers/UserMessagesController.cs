using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.DataTransferObjects;
using Store.Application.Services.Interfaces;
using Store.Domain.Constants;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/user-messages")]
    public class UserMessagesController : ControllerBase
    {
        private readonly IUserMessageService _userMessageService;

        public UserMessagesController(IUserMessageService userMessageService)
        {
            _userMessageService = userMessageService;
        }

        [HttpGet("get-all")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> GetAllUserMessagesAsync(CancellationToken cancellationToken)
        {
            var result = await _userMessageService.GetAllUserMessagesAsync(cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserMessageAsync([FromBody]UserMessageDto userMessageDto, CancellationToken cancellationToken)
        {
            var result = await _userMessageService.AddUserMessageAsync(userMessageDto, cancellationToken);
            return Ok(result);
        }
    }
}
