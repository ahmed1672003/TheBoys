using Microsoft.AspNetCore.Identity;
using TheBoys.Application.Features.Roles.Service;
using TheBoys.Application.Features.Users.Service;
using TheBoys.Application.PipelineBehaviors;
using TheBoys.Domain.Entities.Users;

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
            .AddScoped<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddScoped<IPrtlNewsService, PrtlNewsService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IRoleService, RoleService>()
            .AddScoped<IPrtlLanguageService, PrtlLanguageService>()
            .AddScoped<IContactUsService, ContactUsService>();
        return services;
    }
}
