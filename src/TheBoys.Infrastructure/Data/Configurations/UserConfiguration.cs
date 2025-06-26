using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheBoys.Domain.Entities.Users;

namespace TheBoys.Infrastructure.Data.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //
    }
}
