using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheBoys.API.Entities;

namespace TheBoys.API.Data.Configurations;

public class NewsImageConfiguration : IEntityTypeConfiguration<NewsImage>
{
    public void Configure(EntityTypeBuilder<NewsImage> builder)
    {
        builder.ToTable("NewsImage");

        builder.HasKey(n => n.Id);
        builder.Property(n => n.NewsId).IsRequired();
        builder.Property(n => n.NewsUrl).HasColumnType("nvarchar(max)").IsRequired();

        builder
            .HasOne(n => n.News)
            .WithMany(n => n.NewsImages)
            .HasForeignKey(n => n.NewsId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
