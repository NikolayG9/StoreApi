using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Domain.Repositories;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    internal class ColorRepository(StoreDbContext dbContext) : IColorRepository
    {
        public async Task<List<Color>> GetAllColorsAsync(CancellationToken cancellationToken)
        {
            return await dbContext.Colors.ToListAsync(cancellationToken);
        }

        public Task<Color?> GetColorByIdAsync(int id, CancellationToken cancellationToken)
        {
            return dbContext.Colors.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Color> AddColorAsync(Color color, CancellationToken cancellationToken)
        {
            await dbContext.Colors.AddAsync(color, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return color;
        }

        public async Task<Color> UpdateColorAsync(Color color, CancellationToken cancellationToken)
        {
            dbContext.Colors.Update(color);
            await dbContext.SaveChangesAsync(cancellationToken);

            return color;
        }

        public async Task DeleteColorAsync(Color color, CancellationToken cancellationToken)
        {
            dbContext.Colors.Remove(color);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
