namespace Store.Application.Services.Interfaces
{
    public interface IPdfGeneratorService
    {
        Task<byte[]> GenerateOrderPdfFileAsync(int orderId, CancellationToken cancellationToken);
    }
}
