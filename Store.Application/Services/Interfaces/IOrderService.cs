using Store.Application.DataTransferObjects;

namespace Store.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync(CancellationToken cancellationToken);
        Task<IEnumerable<OrderDto>> GetOrdersByClientId(CancellationToken cancellationToken);
        Task<IEnumerable<OrderDto>> GetAllSoftDeletedOrdersAsync(CancellationToken cancellationToken);
        Task<OrderDto> GetSoftDeletedOrderInformationByIdAsync(int orderId, CancellationToken cancellationToken);
        Task<OrderDto> GetOrderDetailsAsync(int orderId, CancellationToken cancellationToken);
        Task<byte[]?> GetOrderPdfFileAsync(int orderId, CancellationToken cancellationToken);
        Task<OrderDto> AddOrderAsync(OrderDto orderDto, CancellationToken cancellationToken);
        Task<OrderDto> UpdateOrderAsync(OrderDto orderDto, CancellationToken cancellationToken);
        Task<OrderDto> CancelSoftDeletedOrderById(int orderId, CancellationToken cancellationToken);
        Task DeleteOrderAsync(int orderId, CancellationToken cancellationToken);
        Task SoftDeleteOrderAsync(int orderId, CancellationToken cancellationToken);
    }
}
