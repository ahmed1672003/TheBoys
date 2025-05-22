using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheBoys.Infrastructure.Data.Configurations;

internal sealed class AspnetUserConfiguration : IEntityTypeConfiguration<AspnetUser>
{
    public void Configure(EntityTypeBuilder<AspnetUser> builder)
    {
        builder.HasKey(e => e.UserId).HasName("PK__aspnet_U__1788CC4D29572725").IsClustered(false);

        builder.Property(e => e.UserId).ValueGeneratedNever();

        builder
            .HasOne(d => d.Application)
            .WithMany(p => p.AspnetUsers)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__aspnet_Us__Appli__693CA210");

        builder
            .HasMany(d => d.Roles)
            .WithMany(p => p.Users)
            .UsingEntity<Dictionary<string, object>>(
                "AspnetUsersInRole",
                r =>
                    r.HasOne<AspnetRole>()
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__aspnet_Us__RoleI__6A30C649"),
                l =>
                    l.HasOne<AspnetUser>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__aspnet_Us__UserI__6B24EA82"),
                j =>
                {
                    j.HasKey("UserId", "RoleId").HasName("PK__aspnet_U__AF2760AD403A8C7D");
                    j.ToTable("aspnet_UsersInRoles");
                }
            );
    }
}
