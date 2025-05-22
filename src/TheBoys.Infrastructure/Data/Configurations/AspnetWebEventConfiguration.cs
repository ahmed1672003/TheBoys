using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheBoys.Domain.Entities.AspnetWebEvents;

namespace TheBoys.Infrastructure.Data.Configurations;

internal sealed class AspnetWebEventConfiguration : IEntityTypeConfiguration<AspnetWebEvent>
{
    public void Configure(EntityTypeBuilder<AspnetWebEvent> builder)
    {
        builder.HasKey(e => e.EventId).HasName("PK__aspnet_W__7944C81009DE7BCC");
        builder.Property(e => e.EventId).IsFixedLength();
    }
}
