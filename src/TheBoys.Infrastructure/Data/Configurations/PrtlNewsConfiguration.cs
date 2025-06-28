using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheBoys.Infrastructure.Data.Configurations;

internal sealed class PrtlNewsConfiguration : IEntityTypeConfiguration<PrtlNews>
{
    public void Configure(EntityTypeBuilder<PrtlNews> builder)
    {
        builder.HasKey(e => e.NewsId).HasName("Pk_News");

        builder.ToTable(
            "prtl_news",
            tb => tb.HasComment("يحتوي على جميع المعلومات الخاصة بالاخبار\r\n")
        );

        builder.Property(x => x.OwnerId).IsRequired(false);
        builder.Property(e => e.NewsId).HasComment("معرف الخبر");
    }
}
