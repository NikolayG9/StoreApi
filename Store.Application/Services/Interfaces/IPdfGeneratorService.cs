using Store.Domain.Entities;

namespace Store.Application.Services.Interfaces
{
    public interface IPdfGeneratorService
    {
        Task<byte[]> GenerateOrderPdfFileAsync(int orderId, CancellationToken cancellationToken);
        Task<byte[]> GenerateOrderPdfFileAsync(Order order, CancellationToken cancellationToken);
    }
}
