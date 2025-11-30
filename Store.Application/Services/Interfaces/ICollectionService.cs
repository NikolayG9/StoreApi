using Store.Application.DataTransferObjects;

namespace Store.Application.Services.Interfaces
{
    public interface ICollectionService
    {
        Task<IEnumerable<CollectionDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<CollectionDto> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<CollectionDto> CreateAsync(CollectionDto collectionDto, CancellationToken cancellationToken);
        Task<CollectionDto> UpdateAsync(CollectionDto collectionDto, CancellationToken cancellationToken);
        Task DeleteAsync(int collectionId, CancellationToken cancellationToken);
    }
}
