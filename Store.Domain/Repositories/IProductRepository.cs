using Store.Domain.Entities;

namespace Store.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetByCollectionIdAsync(int collectionId, CancellationToken cancellationToken);
        Task<(IEnumerable<Product>, int)> GetByCollectionIdWithParamsAsync(int collectionId, string? searchPhrase, int pageSize, int pageNumber, CancellationToken cancellationToken);
        Task<Product> GetByIdAsync(int productId, CancellationToken cancellationToken);
        Task<Product> CreateAsync(Product product, CancellationToken cancellationToken);
        Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken);
        Task DeleteAsync(Product product, CancellationToken cancellationToken);
    }
}
