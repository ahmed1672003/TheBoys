using TheBoys.Application.Features.Roles.Queries.GetAll;
using TheBoys.Application.Features.Roles.Service;

namespace TheBoys.Application.Features.Roles.Queries;

internal sealed class UserQueriesHandler(IRoleService roleService)
    : IRequestHandler<GetAllRolesQuery, ResponseOf<List<GetAllRolesResult>>>
{
    public async Task<ResponseOf<List<GetAllRolesResult>>> Handle(
        GetAllRolesQuery request,
        CancellationToken cancellationToken
    ) => await roleService.GetAllAsync(cancellationToken);
}
