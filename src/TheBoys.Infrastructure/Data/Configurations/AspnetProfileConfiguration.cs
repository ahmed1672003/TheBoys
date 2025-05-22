using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheBoys.Infrastructure.Data.Configurations;

internal sealed class AspnetProfileConfiguration : IEntityTypeConfiguration<AspnetProfile>
{
    public void Configure(EntityTypeBuilder<AspnetProfile> builder)
    {
        builder.HasKey(e => e.UserId).HasName("PK__aspnet_P__1788CC4C440B1D61");
        builder.Property(e => e.UserId).ValueGeneratedNever();
        builder
            .HasOne(d => d.User)
            .WithOne(p => p.AspnetProfile)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__aspnet_Pr__UserI__6754599E");
    }
}
