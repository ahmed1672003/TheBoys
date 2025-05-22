using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheBoys.Infrastructure.Data.Configurations;

internal sealed class AspnetMembershipConfiguration : IEntityTypeConfiguration<AspnetMembership>
{
    public void Configure(EntityTypeBuilder<AspnetMembership> builder)
    {
        builder.HasKey(e => e.UserId).HasName("PK__aspnet_M__1788CC4D4F7CD00D").IsClustered(false);

        builder.Property(e => e.UserId).ValueGeneratedNever();

        builder
            .HasOne(d => d.Application)
            .WithMany(p => p.AspnetMemberships)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__aspnet_Me__Appli__619B8048");

        builder
            .HasOne(d => d.User)
            .WithOne(p => p.AspnetMembership)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__aspnet_Me__UserI__628FA481");
    }
}
