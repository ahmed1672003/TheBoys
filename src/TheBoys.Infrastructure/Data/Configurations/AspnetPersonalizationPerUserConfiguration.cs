using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheBoys.Infrastructure.Data.Configurations;

internal sealed class AspnetPersonalizationPerUserConfiguration
    : IEntityTypeConfiguration<AspnetPersonalizationPerUser>
{
    public void Configure(EntityTypeBuilder<AspnetPersonalizationPerUser> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__aspnet_P__3214EC0647DBAE45").IsClustered(false);

        builder.Property(e => e.Id).ValueGeneratedNever();

        builder
            .HasOne(d => d.Path)
            .WithMany(p => p.AspnetPersonalizationPerUsers)
            .HasConstraintName("FK__aspnet_Pe__PathI__656C112C");

        builder
            .HasOne(d => d.User)
            .WithMany(p => p.AspnetPersonalizationPerUsers)
            .HasConstraintName("FK__aspnet_Pe__UserI__66603565");
    }
}
