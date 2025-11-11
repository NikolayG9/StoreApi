using Microsoft.AspNetCore.Identity;

namespace Store.Domain.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<IdentityRole>> GetRolesAsync(CancellationToken cancellationToken);
        Task<IdentityRole?> GetRoleByIdAsync(string roleId, CancellationToken cancellationToken);
        Task<IdentityRole> AddRoleAsync(IdentityRole role, CancellationToken cancellationToken);
        Task<IdentityRole> UpdateRoleAsync(IdentityRole role, CancellationToken cancellationToken);
        Task DeleteRoleAsync(IdentityRole role, CancellationToken cancellationToken);
    }
}
