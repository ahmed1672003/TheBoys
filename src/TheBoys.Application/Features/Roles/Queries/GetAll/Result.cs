using TheBoys.Shared.Enums.Roles;

namespace TheBoys.Application.Features.Roles.Queries.GetAll;

public sealed record GetAllRolesResult
{
    public int Id { get; set; }
    public RoleType Type { get; set; }
    public string Name => Type.ToString();
}
