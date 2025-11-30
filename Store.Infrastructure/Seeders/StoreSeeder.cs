using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Domain.Constants;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Seeders
{
    internal class StoreSeeder(StoreDbContext dbContext) : IStoreSeeder
    {
        public async Task Seed()
        {
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                await dbContext.Database.MigrateAsync();
            }

            if (await dbContext.Database.CanConnectAsync())
            {
                if (await dbContext.Roles.AnyAsync() == false)
                {
                    var roles = GetRoles();
                    dbContext.Roles.AddRange(roles);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles = 
            [
                new (UserRole.Client)
                {
                    NormalizedName = UserRole.Client.ToUpper()
                },
                new (UserRole.Admin)
                {
                    NormalizedName = UserRole.Admin.ToUpper()
                }
            ];

            return roles;
        }
    }
}
