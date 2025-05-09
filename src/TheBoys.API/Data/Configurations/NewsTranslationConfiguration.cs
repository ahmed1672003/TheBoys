using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheBoys.API.Entities;

namespace TheBoys.API.Data.Configurations;

public class NewsTranslationConfiguration : IEntityTypeConfiguration<NewsTranslation>
{
    public void Configure(EntityTypeBuilder<NewsTranslation> builder)
    {
        builder.ToTable("prtl_News_Translations");

        builder.HasKey(n => n.Id);
        builder.Property(n => n.NewsHead).HasColumnType("nvarchar(max)").IsRequired();
        builder.Property(n => n.NewsAbbr).HasColumnType("nvarchar(max)").IsRequired();
        builder.Property(n => n.NewsBody).HasColumnType("nvarchar(max)").IsRequired();
        builder.Property(n => n.NewsSource).HasColumnType("nvarchar(max)").IsRequired();
        builder.Property(n => n.LangId).IsRequired();
        builder.Property(n => n.ImgAlt).HasColumnType("nvarchar(max)").IsRequired();

        builder
            .HasOne(n => n.News)
            .WithMany(n => n.NewsTranslations)
            .HasForeignKey(n => n.NewsId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
