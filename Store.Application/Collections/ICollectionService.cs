using Store.Application.Collections.Dtos;

namespace Store.Application.Collections
{
    public interface ICollectionService
    {
        Task<IEnumerable<CollectionDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<CollectionDto> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<CollectionDto> CreateAsync(CollectionDto collectionDto, CancellationToken cancellationToken);
        Task<CollectionDto> UpdateAsync(CollectionDto collectionDto, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int collectionId, CancellationToken cancellationToken);
    }
}
