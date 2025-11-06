using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Domain.Repositories;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    internal class CollectionRepository(StoreDbContext dbContext) : ICollectionRepository
    {
        public async Task<IEnumerable<Collection>> GetAllAsync(CancellationToken cancellationToken)
        {
            var collections = await dbContext.Collections.ToListAsync(cancellationToken);
            return collections;
        }

        public async Task<Collection> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var collection = await dbContext.Collections
                                            .Include(x => x.Products)
                                            .FirstOrDefaultAsync(x => x.Id == id);
            return collection;
        }

        public async Task<Collection> CreateAsync(Collection collection, CancellationToken cancellationToken)
        {
            collection.CreatedAt = DateTime.Now;

            await dbContext.Collections.AddAsync(collection, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return collection;
        }

        public async Task<Collection> UpdateAsync(Collection collection, CancellationToken cancellationToken)
        {
            dbContext.Collections.Update(collection);
            await dbContext.SaveChangesAsync(cancellationToken);

            return collection;
        }

        public async Task DeleteAsync(Collection collection, CancellationToken cancellationToken)
        {
            dbContext.Collections.Remove(collection);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
