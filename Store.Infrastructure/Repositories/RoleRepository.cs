using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Domain.Repositories;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    internal class RoleRepository(StoreDbContext dbContext) : IRoleRepository
    {
        public async Task<IEnumerable<IdentityRole>> GetRolesAsync(CancellationToken cancellationToken)
        {
            var roles = await dbContext.Roles.ToListAsync();
            return roles;
        }

        public async Task<IdentityRole?> GetRoleByIdAsync(string id, CancellationToken cancellationToken)
        {
            return await dbContext.Roles.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IdentityRole> AddRoleAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            await dbContext.Roles.AddAsync(role, cancellationToken);
            await dbContext.SaveChangesAsync();
            return role;
        }

        public async Task<IdentityRole> UpdateRoleAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            dbContext.Roles.Update(role);
            await dbContext.SaveChangesAsync();
            return role;
        }

        public async Task DeleteRoleAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            dbContext.Roles.Remove(role);
            await dbContext.SaveChangesAsync();
        }
    }
}
