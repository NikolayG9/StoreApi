using Store.Domain.Entities;

namespace Store.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Order>> GetOrdersByClientIdAsync(string clientId, CancellationToken cancellationToken);
        Task<Order?> GetOrderDetailsByOrderIdAsync(int orderId, CancellationToken cancellationToken);
        Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken);
        Task<Order> UpdateOrderAsync(Order order, CancellationToken cancellationToken);
        Task DeleteOrderAsync(Order order, CancellationToken cancellationToken);
        Task SoftDeleteOrderAsync(Order order, CancellationToken cancellationToken);
    }
}
