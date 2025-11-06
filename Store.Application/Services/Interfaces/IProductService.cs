using Store.Application.DataTransferObjects;

namespace Store.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetByCollectionIdAsync(int collectionId, CancellationToken cancellationToken);
        Task<ProductDto> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<ProductDto> CreateAsync(ProductDto productDto, CancellationToken cancellationToken);
        Task<ProductDto> UpdateAsync(ProductDto productDto, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
