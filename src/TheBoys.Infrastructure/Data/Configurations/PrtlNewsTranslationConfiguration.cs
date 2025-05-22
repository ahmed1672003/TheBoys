using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheBoys.Infrastructure.Data.Configurations;

internal sealed class PrtlNewsTranslationConfiguration
    : IEntityTypeConfiguration<PrtlNewsTranslation>
{
    public void Configure(EntityTypeBuilder<PrtlNewsTranslation> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_news_trans");

        builder.ToTable(
            "prtl_News_Translations",
            tb => tb.HasComment("جدول ترجمة الاخبار لاكثر من لغة")
        );

        builder.Property(e => e.Id).HasComment("معرف عام");
        builder.Property(e => e.LangId).HasComment("معرف اللغة");
        builder.Property(e => e.NewsAbbr).HasComment("مختصر الخبر");
        builder.Property(e => e.NewsBody).HasComment("تفاصيل الخير");
        builder.Property(e => e.NewsHead).HasComment("عنوان الاخبار");
        builder.Property(e => e.NewsId).HasComment("معرف الخبر");
        builder.Property(e => e.NewsSource).HasComment("مصدر الخبر");
        builder
            .HasOne(d => d.Lang)
            .WithMany(p => p.PrtlNewsTranslations)
            .HasConstraintName("FK_news_trans_Languages");
        builder
            .HasOne(d => d.News)
            .WithMany(p => p.PrtlNewsTranslations)
            .HasConstraintName("FK_news_trans_News");
    }
}
