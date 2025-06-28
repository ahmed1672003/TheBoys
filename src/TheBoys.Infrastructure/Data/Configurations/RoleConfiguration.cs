using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheBoys.Domain.Entities.Roles;

namespace TheBoys.Infrastructure.Data.Configurations;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles", "identity");
        builder.HasKey(x => x.Id);
    }
}
