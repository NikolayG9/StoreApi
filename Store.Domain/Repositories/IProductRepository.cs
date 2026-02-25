using Store.Domain.Entities;

namespace Store.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetByCollectionIdAsync(int collectionId, CancellationToken cancellationToken);
        Task<(IEnumerable<Product>, int)> GetByCollectionIdWithParamsAsync(int collectionId, string? searchPhrase, int pageSize, int pageNumber, CancellationToken cancellationToken);
        Task<Product?> GetByIdAsync(int productId, CancellationToken cancellationToken);
        Task<bool> IsAnyProductByIdAsync(int productId, CancellationToken cancellationToken);
        Task<Product> AddProductAsync(Product product, CancellationToken cancellationToken);
        Task AddProductImageAsync(Image image, CancellationToken cancellationToken);
        Task<Product> UpdateProductAsync(Product product, CancellationToken cancellationToken);
        Task DeleteAsync(Product product, CancellationToken cancellationToken);
        Task DeleteProductImageAsync(Image image, CancellationToken cancellationToken);
    }
}
