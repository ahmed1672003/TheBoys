using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheBoys.Infrastructure.Data.Configurations;

internal sealed class AspnetPersonalizationAllUserConfiguration
    : IEntityTypeConfiguration<AspnetPersonalizationAllUser>
{
    public void Configure(EntityTypeBuilder<AspnetPersonalizationAllUser> builder)
    {
        builder.HasKey(e => e.PathId).HasName("PK__aspnet_P__CD67DC594BAC3F29");

        builder.Property(e => e.PathId).ValueGeneratedNever();

        builder
            .HasOne(d => d.Path)
            .WithOne(p => p.AspnetPersonalizationAllUser)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__aspnet_Pe__PathI__6477ECF3");
    }
}
