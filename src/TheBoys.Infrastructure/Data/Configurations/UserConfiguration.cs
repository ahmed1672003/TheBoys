using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheBoys.Domain.Entities.Users;

namespace TheBoys.Infrastructure.Data.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "identity");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Username).IsUnique(true);
        builder.HasIndex(x => x.Email).IsUnique(true);
    }
}
