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

        [Authorize(Roles = UserRole.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetOrdersByClientId([FromQuery] string userId, CancellationToken cancellationToken)
        {
            var result = await _orderService.GetOrdersByClientId(userId, cancellationToken);
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
        [HttpGet("get-pdf/{orderId}")]
        public async Task<IActionResult> GetOrderPdfFileAsync([FromRoute] int orderId, CancellationToken cancellationToken)
        {
            var pdfData = await _orderService.GetOrderPdfFileAsync(orderId, cancellationToken);
            return Ok(pdfData);
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

        [Authorize]
        [HttpPut("update-status/{orderId}")]
        public async Task<IActionResult> UpdateOrderStatusAsync([FromRoute] int orderId, [FromQuery] string status, CancellationToken cancellationToken)
        {
            await _orderService.UpdateOrderStatusAsync(orderId, status, cancellationToken);
            return NoContent();
        }
        

        [Authorize(Roles = UserRole.Admin)]
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrderAsync([FromRoute] int orderId, CancellationToken cancellationToken)
        {
            await _orderService.DeleteOrderAsync(orderId, cancellationToken);
            return NoContent();
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpGet("soft-delete/get-all")]
        public async Task<IActionResult> GetAllSoftDeletedOrdersAsync(CancellationToken cancellationToken)
        {
            var result = await _orderService.GetAllSoftDeletedOrdersAsync(cancellationToken);
            return Ok(result);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpGet("soft-delete/{orderId}")]
        public async Task<IActionResult> GetSoftDeleteOrderInformationAsync([FromRoute] int orderId, CancellationToken cancellationToken)
        {
            var result = await _orderService.GetSoftDeletedOrderInformationByIdAsync(orderId, cancellationToken);
            return Ok(result);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpDelete("soft-delete/{orderId}")]
        public async Task<IActionResult> SoftDeleteOrderAsync([FromRoute] int orderId, CancellationToken cancellationToken)
        {
            await _orderService.SoftDeleteOrderAsync(orderId, cancellationToken);
            return NoContent();
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpPut("soft-delete-cancel/{orderId}")]
        public async Task<IActionResult> CancelSoftDeleteOrderAsync([FromRoute] int orderId, CancellationToken cancellationToken)
        {
            var result = await _orderService.CancelSoftDeletedOrderById(orderId, cancellationToken);
            return Ok(result);
        }
    }
}
