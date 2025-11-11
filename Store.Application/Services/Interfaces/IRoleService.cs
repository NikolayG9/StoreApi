using Store.Application.DataTransferObjects;

namespace Store.Application.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetRolesAsync(CancellationToken cancellationToken);
        Task<RoleDto> AddRoleAsync(RoleDto roleDto, CancellationToken cancellationToken);
        Task<RoleDto> UpdateRoleAsync(RoleDto roleDto, CancellationToken cancellationToken);
        Task DeleteRoleAsync(string roleId, CancellationToken cancellationToken);
    }
}
