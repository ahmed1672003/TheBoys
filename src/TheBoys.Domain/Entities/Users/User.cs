using TheBoys.Domain.Entities.Roles;

namespace TheBoys.Domain.Entities.Users;

public sealed class User
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string HashedPassword { get; set; }
    public Role Role { get; set; }
}
