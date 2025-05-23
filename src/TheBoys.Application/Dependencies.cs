using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TheBoys.Application.Features.News.Service;

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
        services.AddScoped<IPrtlNewsService, PrtlNewsService>();
        return services;
    }
}
