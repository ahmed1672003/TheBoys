using Microsoft.EntityFrameworkCore;
using TheBoys.Domain.Entities.AspnetApplications;
using TheBoys.Domain.Entities.AspnetMemberships;
using TheBoys.Domain.Entities.AspnetPathes;
using TheBoys.Domain.Entities.AspnetPersonalizationAllUsers;
using TheBoys.Domain.Entities.AspnetPersonalizationPerUsers;
using TheBoys.Domain.Entities.AspnetProfiles;
using TheBoys.Domain.Entities.AspnetRoles;
using TheBoys.Domain.Entities.AspnetUsers;
using TheBoys.Domain.Entities.AspnetWebEvents;
using TheBoys.Domain.Entities.Languages;
using TheBoys.Domain.Entities.News;
using TheBoys.Domain.Entities.NewsTranslations;
using TheBoys.Domain.Views;

namespace TheBoys.API.Models;

public partial class MnfPortalsContext : DbContext
{
    public MnfPortalsContext() { }

    public MnfPortalsContext(DbContextOptions<MnfPortalsContext> options)
        : base(options) { }

    public virtual DbSet<AspnetApplication> AspnetApplications { get; set; }

    public virtual DbSet<AspnetMembership> AspnetMemberships { get; set; }

    public virtual DbSet<AspnetPath> AspnetPaths { get; set; }

    public virtual DbSet<AspnetPersonalizationAllUser> AspnetPersonalizationAllUsers { get; set; }

    public virtual DbSet<AspnetPersonalizationPerUser> AspnetPersonalizationPerUsers { get; set; }

    public virtual DbSet<AspnetProfile> AspnetProfiles { get; set; }

    public virtual DbSet<AspnetRole> AspnetRoles { get; set; }

    public virtual DbSet<AspnetUser> AspnetUsers { get; set; }

    public virtual DbSet<AspnetWebEvent> AspnetWebEventEvents { get; set; }

    public virtual DbSet<PrtlLanguage> PrtlLanguages { get; set; }

    public virtual DbSet<PrtlNews> PrtlNews { get; set; }

    public virtual DbSet<PrtlNewsTranslation> PrtlNewsTranslations { get; set; }

    public virtual DbSet<ZVwWafdinNews> ZVwWafdinNews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        =>
        optionsBuilder.UseSqlServer(
            "Server=193.227.24.22,1433;Database=MnfPortals;User Id=yahya;Password=iFatma@2025!!;TrustServerCertificate=True;Encrypt=True;Connection Timeout=30;"
        );

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Arabic_CI_AS");

        modelBuilder.Entity<AspnetApplication>(entity =>
        {
            entity
                .HasKey(e => e.ApplicationId)
                .HasName("PK__aspnet_A__C93A4C9821B6055D")
                .IsClustered(false);

            entity.Property(e => e.ApplicationId).ValueGeneratedNever();
        });

        modelBuilder.Entity<AspnetMembership>(entity =>
        {
            entity
                .HasKey(e => e.UserId)
                .HasName("PK__aspnet_M__1788CC4D4F7CD00D")
                .IsClustered(false);

            entity.Property(e => e.UserId).ValueGeneratedNever();

            entity
                .HasOne(d => d.Application)
                .WithMany(p => p.AspnetMemberships)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__aspnet_Me__Appli__619B8048");

            entity
                .HasOne(d => d.User)
                .WithOne(p => p.AspnetMembership)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__aspnet_Me__UserI__628FA481");
        });

        modelBuilder.Entity<AspnetPath>(entity =>
        {
            entity
                .HasKey(e => e.PathId)
                .HasName("PK__aspnet_P__CD67DC5825869641")
                .IsClustered(false);

            entity.Property(e => e.PathId).ValueGeneratedNever();

            entity
                .HasOne(d => d.Application)
                .WithMany(p => p.AspnetPaths)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__aspnet_Pa__Appli__6383C8BA");
        });

        modelBuilder.Entity<AspnetPersonalizationAllUser>(entity =>
        {
            entity.HasKey(e => e.PathId).HasName("PK__aspnet_P__CD67DC594BAC3F29");

            entity.Property(e => e.PathId).ValueGeneratedNever();

            entity
                .HasOne(d => d.Path)
                .WithOne(p => p.AspnetPersonalizationAllUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__aspnet_Pe__PathI__6477ECF3");
        });

        modelBuilder.Entity<AspnetPersonalizationPerUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__aspnet_P__3214EC0647DBAE45").IsClustered(false);

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity
                .HasOne(d => d.Path)
                .WithMany(p => p.AspnetPersonalizationPerUsers)
                .HasConstraintName("FK__aspnet_Pe__PathI__656C112C");

            entity
                .HasOne(d => d.User)
                .WithMany(p => p.AspnetPersonalizationPerUsers)
                .HasConstraintName("FK__aspnet_Pe__UserI__66603565");
        });

        modelBuilder.Entity<AspnetProfile>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__aspnet_P__1788CC4C440B1D61");

            entity.Property(e => e.UserId).ValueGeneratedNever();

            entity
                .HasOne(d => d.User)
                .WithOne(p => p.AspnetProfile)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__aspnet_Pr__UserI__6754599E");
        });

        modelBuilder.Entity<AspnetRole>(entity =>
        {
            entity
                .HasKey(e => e.RoleId)
                .HasName("PK__aspnet_R__8AFACE1B2D27B809")
                .IsClustered(false);

            entity.Property(e => e.RoleId).ValueGeneratedNever();

            entity
                .HasOne(d => d.Application)
                .WithMany(p => p.AspnetRoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__aspnet_Ro__Appli__68487DD7");
        });

        modelBuilder.Entity<AspnetUser>(entity =>
        {
            entity
                .HasKey(e => e.UserId)
                .HasName("PK__aspnet_U__1788CC4D29572725")
                .IsClustered(false);

            entity.Property(e => e.UserId).ValueGeneratedNever();

            entity
                .HasOne(d => d.Application)
                .WithMany(p => p.AspnetUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__aspnet_Us__Appli__693CA210");

            entity
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
        });

        modelBuilder.Entity<AspnetWebEvent>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__aspnet_W__7944C81009DE7BCC");

            entity.Property(e => e.EventId).IsFixedLength();
        });

        modelBuilder.Entity<PrtlLanguage>(entity =>
        {
            entity.HasKey(e => e.LangId).HasName("Pk_Languages");

            entity.ToTable(
                "prtl_Languages",
                tb => tb.HasComment("يحتوى على اللغات المعتمدة فى كل تفاصيل قاعدة البيانات")
            );

            entity.Property(e => e.LangId).HasComment("معرف  اللغة المستخدمة");
            entity.Property(e => e.Lcid).HasComment("كود اللهجة المحلية");
        });

        modelBuilder.Entity<PrtlNews>(entity =>
        {
            entity.HasKey(e => e.NewsId).HasName("Pk_News");

            entity.ToTable(
                "prtl_news",
                tb => tb.HasComment("يحتوي على جميع المعلومات الخاصة بالاخبار\r\n")
            );

            entity.Property(e => e.NewsId).HasComment("معرف الخبر");
        });

        modelBuilder.Entity<PrtlNewsTranslation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_news_trans");

            entity.ToTable(
                "prtl_News_Translations",
                tb => tb.HasComment("جدول ترجمة الاخبار لاكثر من لغة")
            );

            entity.Property(e => e.Id).HasComment("معرف عام");
            entity.Property(e => e.LangId).HasComment("معرف اللغة");
            entity.Property(e => e.NewsAbbr).HasComment("مختصر الخبر");
            entity.Property(e => e.NewsBody).HasComment("تفاصيل الخير");
            entity.Property(e => e.NewsHead).HasComment("عنوان الاخبار");
            entity.Property(e => e.NewsId).HasComment("معرف الخبر");
            entity.Property(e => e.NewsSource).HasComment("مصدر الخبر");

            entity
                .HasOne(d => d.Lang)
                .WithMany(p => p.PrtlNewsTranslations)
                .HasConstraintName("FK_news_trans_Languages");

            entity
                .HasOne(d => d.News)
                .WithMany(p => p.PrtlNewsTranslations)
                .HasConstraintName("FK_news_trans_News");
        });

        modelBuilder.Entity<ZVwWafdinNews>(entity =>
        {
            entity.ToView("zVW_Wafdin_News");

            entity.Property(e => e.NewsId).ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
