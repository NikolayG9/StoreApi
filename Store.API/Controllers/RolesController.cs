using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.DataTransferObjects;
using Store.Application.Services.Interfaces;
using Store.Domain.Constants;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/roles")]
    [Authorize(Roles = UserRole.Admin)]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRolesAsync(CancellationToken cancellationToken)
        {
            var result = await _roleService.GetRolesAsync(cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoleAsync([FromBody] RoleDto roleDto, CancellationToken cancellationToken)
        {
            var result = await _roleService.AddRoleAsync(roleDto, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoleAsync([FromBody] RoleDto roleDto, CancellationToken cancellationToken)
        {
            var result = await _roleService.UpdateRoleAsync(roleDto, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleAsync([FromRoute] string id, CancellationToken cancellationToken)
        {
            await _roleService.DeleteRoleAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
