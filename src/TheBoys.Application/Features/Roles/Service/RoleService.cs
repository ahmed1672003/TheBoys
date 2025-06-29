using TheBoys.Application.Features.Roles.Queries.GetAll;
using TheBoys.Domain.Entities.Roles;

namespace TheBoys.Application.Features.Roles.Service;

public sealed class RoleService(IRoleRepository roleRepository) : IRoleService
{
    public async Task<ResponseOf<List<GetAllRolesResult>>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        var roles = await roleRepository.GetAllAsync(cancellationToken);
        return new ResponseOf<List<GetAllRolesResult>>()
        {
            Success = true,
            Result = roles
                .Select(x => new GetAllRolesResult() { Id = x.Id, Type = x.Type })
                .ToList(),
        };
    }
}
