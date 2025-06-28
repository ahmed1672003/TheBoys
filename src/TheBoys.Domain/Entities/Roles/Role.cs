using TheBoys.Shared.Enums.Roles;

namespace TheBoys.Domain.Entities.Roles;

public sealed class Role
{
    public int Id { get; set; }
    public RoleType Type { get; set; }
}
