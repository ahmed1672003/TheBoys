namespace TheBoys.Infrastructure;

public static class Dependencies
{
    public static IServiceCollection RegisterInfrastructureDependencies(
        this IServiceCollection services,
        IConfiguration configuration,
        EnvironmentType environment
    )
    {
        services.AddDbContext<MnfPortalsDbContext>(cfg =>
            cfg.UseSqlServer(
                configuration.GetConnectionString(
                    environment == EnvironmentType.Development
                        ? "LocalDatabaseConnection"
                        : "ProductionDatabaseConnection"
                )
            )
        );
        services
            .AddScoped(typeof(IRepository<>), typeof(Repository<>))
            .AddScoped<IPrtlLanguageRepository, PrtlLanguageRepository>()
            .AddScoped<IPrtlNewsRepository, PrtlNewsRepository>()
            .AddScoped<IPrtlNewsTranslationRepository, PrtlNewsTranslationRepository>();
        return services;
    }
}
