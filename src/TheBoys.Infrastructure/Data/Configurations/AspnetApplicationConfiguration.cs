using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheBoys.Infrastructure.Data.Configurations;

internal sealed class AspnetApplicationConfiguration : IEntityTypeConfiguration<AspnetApplication>
{
    public void Configure(EntityTypeBuilder<AspnetApplication> builder)
    {
        builder
            .HasKey(e => e.ApplicationId)
            .HasName("PK__aspnet_A__C93A4C9821B6055D")
            .IsClustered(false);

        builder.Property(e => e.ApplicationId).ValueGeneratedNever();
    }
}
