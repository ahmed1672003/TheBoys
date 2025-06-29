using TheBoys.Application.Features.Roles.Queries.GetAll;

namespace TheBoys.Application.Features.Roles.Service;

public interface IRoleService
{
    Task<ResponseOf<List<GetAllRolesResult>>> GetAllAsync(
        CancellationToken cancellationToken = default
    );
}
