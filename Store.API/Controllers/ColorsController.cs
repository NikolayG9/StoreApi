using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.DataTransferObjects;
using Store.Application.Services.Interfaces;
using Store.Domain.Constants;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/colors")]
    public class ColorsController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorsController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllColorsAsync(CancellationToken cancellationToken)
        {
            var result = await _colorService.GetAllColorsAsync(cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetColorByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _colorService.GetColorByIdAsync(id, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> AddColorAsync([FromBody] ColorDto color, CancellationToken cancellationToken)
        {
            var result = await _colorService.AddColorAsync(color, cancellationToken);
            return Ok(result);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> UpdateColorAsync([FromRoute] int id, [FromBody] ColorDto color, CancellationToken cancellationToken)
        {
            var result = await _colorService.UpdateColorAsync(color, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> DeleteColorAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _colorService.DeleteColorAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
