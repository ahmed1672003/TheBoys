using System.Reflection;

namespace TheBoys.Infrastructure.Data;

public partial class MnfPortalsDbContext : DbContext
{
    public MnfPortalsDbContext(DbContextOptions<MnfPortalsDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Arabic_CI_AS");
        OnModelCreatingPartial(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
