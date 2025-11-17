using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Domain.Repositories;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    internal class OrderRepository(StoreDbContext dbContext) : IOrderRepository
    {
        public async Task<IEnumerable<Order>> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders.Include(x => x.OrderInformation).ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<Order>> GetOrdersByClientIdAsync(string clientId, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders.Include(x => x.OrderInformation).Where(x => x.UserId == clientId).ToListAsync();
            return orders;
        }

        public async Task<Order?> GetOrderDetailsByOrderIdAsync(int orderId, CancellationToken cancellationToken)
        {
            var orderDetails = await dbContext.Orders
                                              .Include(x => x.OrderInformation)
                                              .Include(x => x.OrderedProducts)
                                              .FirstOrDefaultAsync(x => x.Id == orderId);

            return orderDetails;
        }

        public async Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken)
        {
            await dbContext.Orders.AddAsync(order, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return order;
        }

        public async Task<Order> UpdateOrderAsync(Order order, CancellationToken cancellationToken)
        {
            dbContext.Orders.Update(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            return order;
        }

        public async Task DeleteOrderAsync(Order order, CancellationToken cancellationToken)
        {
            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task SoftDeleteOrderAsync(Order order, CancellationToken cancellationToken)
        {
            order.IsSoftDeleted = true;
            dbContext.Orders.Update(order);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
