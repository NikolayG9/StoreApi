using Store.Application.DataTransferObjects;

namespace Store.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync(CancellationToken cancellationToken);
        Task<IEnumerable<OrderDto>> GetOrdersByClientId(CancellationToken cancellationToken);
        Task<OrderDto> GetOrderDetailsAsync(int orderId, CancellationToken cancellationToken);
        Task<OrderDto> AddOrderAsync(OrderDto orderDto, CancellationToken cancellationToken);
        Task<OrderDto> UpdateOrderAsync(OrderDto orderDto, CancellationToken cancellationToken);
        Task<bool> DeleteOrderAsync(int orderId, CancellationToken cancellationToken);
        Task<bool> SoftDeleteOrderAsync(int orderId, CancellationToken cancellationToken);
    }
}
