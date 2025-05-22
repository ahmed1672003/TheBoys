using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheBoys.Infrastructure.Data.Configurations;

internal sealed class AspnetPathConfiguration : IEntityTypeConfiguration<AspnetPath>
{
    public void Configure(EntityTypeBuilder<AspnetPath> builder)
    {
        builder.HasKey(e => e.PathId).HasName("PK__aspnet_P__CD67DC5825869641").IsClustered(false);

        builder.Property(e => e.PathId).ValueGeneratedNever();

        builder
            .HasOne(d => d.Application)
            .WithMany(p => p.AspnetPaths)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__aspnet_Pa__Appli__6383C8BA");
    }
}
