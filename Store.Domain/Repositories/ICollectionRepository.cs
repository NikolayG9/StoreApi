using Store.Domain.Entities;

namespace Store.Domain.Repositories
{
    public interface ICollectionRepository
    {
        Task<IEnumerable<Collection>> GetAllAsync(CancellationToken cancellationToken);
        Task<Collection> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Collection> CreateAsync(Collection collection, CancellationToken cancellationToken);
        Task<Collection> UpdateAsync(Collection collection, CancellationToken cancellationToken);
        Task DeleteAsync(Collection collection, CancellationToken cancellationToken);
    }
}
