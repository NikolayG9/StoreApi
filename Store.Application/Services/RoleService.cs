using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Store.Application.DataTransferObjects;
using Store.Application.Services.Interfaces;
using Store.Domain.Exceptions;
using Store.Domain.Repositories;

namespace Store.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<RoleService> _logger;
        private readonly IMapper _mapper;

        public RoleService(
            IRoleRepository roleRepository,
            ILogger<RoleService> logger,
            IMapper mapper)
        {
            _roleRepository = roleRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> GetRolesAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get All Roles");
            var roles = await _roleRepository.GetRolesAsync(cancellationToken);
            return _mapper.Map<List<RoleDto>>(roles);
        }

        public async Task<RoleDto> AddRoleAsync(RoleDto roleDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Add New Role - {roleDto.Name}");
            var role = _mapper.Map<IdentityRole>(roleDto);
            role.Id = Guid.NewGuid().ToString();
            var newRole = await _roleRepository.AddRoleAsync(role, cancellationToken);

            return _mapper.Map<RoleDto>(newRole);
        }

        public async Task<RoleDto> UpdateRoleAsync(RoleDto roleDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Update Role - {roleDto.Name}");
            
            if (await _roleRepository.IsAnyRoleByIdAsync(roleDto.Id, cancellationToken))
            {
                throw new NotFoundException(nameof(RoleDto), roleDto.Id);
            }

            var role = _mapper.Map<IdentityRole>(roleDto);
            var updatedRole = await _roleRepository.UpdateRoleAsync(role, cancellationToken);

            return _mapper.Map<RoleDto>(updatedRole);
        }

        public async Task DeleteRoleAsync(string roleId, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetRoleByIdAsync(roleId, cancellationToken);
            if (role == null)
            {
                throw new NotFoundException(nameof(RoleDto), roleId);
            }

            _logger.LogInformation($"Delete Role - {role.Name}");
            await _roleRepository.DeleteRoleAsync(role, cancellationToken);
        }
    }
}
