using TheBoys.Application.PipelineBehaviors;

namespace TheBoys.Application;

public static class Dependencies
{
    public static IServiceCollection RegisterApplicationDependencies(
        this IServiceCollection services
    )
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
        );
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services
            .AddScoped<IPrtlNewsService, PrtlNewsService>()
            .AddScoped<IPrtlLanguageService, PrtlLanguageService>()
            .AddScoped<IContactUsService, ContactUsService>();
        return services;
    }
}
