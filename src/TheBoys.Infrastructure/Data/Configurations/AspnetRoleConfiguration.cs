using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheBoys.Infrastructure.Data.Configurations;

internal sealed class AspnetRoleConfiguration : IEntityTypeConfiguration<AspnetRole>
{
    public void Configure(EntityTypeBuilder<AspnetRole> builder)
    {
        builder.HasKey(e => e.RoleId).HasName("PK__aspnet_R__8AFACE1B2D27B809").IsClustered(false);

        builder.Property(e => e.RoleId).ValueGeneratedNever();

        builder
            .HasOne(d => d.Application)
            .WithMany(p => p.AspnetRoles)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__aspnet_Ro__Appli__68487DD7");
    }
}
