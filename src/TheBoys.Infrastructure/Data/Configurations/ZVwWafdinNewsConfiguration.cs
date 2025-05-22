using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheBoys.Infrastructure.Data.Configurations;

internal sealed class ZVwWafdinNewsConfiguration : IEntityTypeConfiguration<ZVwWafdinNews>
{
    public void Configure(EntityTypeBuilder<ZVwWafdinNews> builder)
    {
        builder.ToView("zVW_Wafdin_News");
        builder.Property(e => e.NewsId).ValueGeneratedOnAdd();
    }
}
