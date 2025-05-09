using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheBoys.API.Entities;

namespace TheBoys.API.Data.Configurations;

public class NewsConfiguration : IEntityTypeConfiguration<News>
{
    public void Configure(EntityTypeBuilder<News> builder)
    {
        builder.ToTable("prtl_News");

        builder.HasKey(n => n.NewsId);
        builder.Property(n => n.NewsDate).IsRequired();
        builder.Property(n => n.NewsImg).HasColumnType("nvarchar(max)").IsRequired();
        builder.Property(n => n.OwnerId).IsRequired();
        builder.Property(n => n.IsFeature).HasDefaultValue(false);

        builder
            .HasMany(n => n.NewsImages)
            .WithOne(n => n.News)
            .HasForeignKey(n => n.NewsId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(n => n.NewsTranslations)
            .WithOne(n => n.News)
            .HasForeignKey(n => n.NewsId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
