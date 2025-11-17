using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.DataTransferObjects;
using Store.Application.Services.Interfaces;
using Store.Domain.Constants;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOrdersByClientId(CancellationToken cancellationToken)
        {
            var result = await _orderService.GetOrdersByClientId(cancellationToken);
            return Ok(result);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            var result = await _orderService.GetAllOrdersAsync(cancellationToken);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderDetailsAsync([FromRoute] int orderId, CancellationToken cancellationToken)
        {
            var result = await _orderService.GetOrderDetailsAsync(orderId, cancellationToken);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddOrderAsync([FromBody] OrderDto orderDto, CancellationToken cancellationToken)
        {
            var result = await _orderService.AddOrderAsync(orderDto, cancellationToken);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrderAsync([FromBody] OrderDto orderDto, CancellationToken cancellationToken)
        {
            var result = await _orderService.UpdateOrderAsync(orderDto, cancellationToken);
            return Ok(result);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrderAsync([FromRoute] int orderId, CancellationToken cancellationToken)
        {
            var result = await _orderService.DeleteOrderAsync(orderId, cancellationToken);
            return Ok(result);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpDelete("soft-delete/{orderId}")]
        public async Task<IActionResult> SoftDeleteOrderAsync([FromRoute] int orderId, CancellationToken cancellationToken)
        {
            var result = await _orderService.SoftDeleteOrderAsync(orderId, cancellationToken);
            return Ok(result);
        }
    }
}
