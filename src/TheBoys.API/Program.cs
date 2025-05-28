using System.Reflection;
using System.Threading.RateLimiting;
using Microsoft.Extensions.Localization;
using Microsoft.OpenApi.Models;
using TheBoys.API.Localization;
using TheBoys.API.Middlewares;
using TheBoys.API.Seeding;
using TheBoys.Application;
using TheBoys.Application.Contexts;
using TheBoys.Application.Externals;
using TheBoys.Application.Settings;
using TheBoys.Infrastructure;
using TheBoys.Shared.Abstractions;
using TheBoys.Shared.Enums;
using TheBoys.Shared.Externals.Email;

namespace TheBoys.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc(
                "the.boys.api",
                new OpenApiInfo { Title = "the.boys.api", Version = "v1", }
            );
            s.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description =
                        "If you hit endpoints from swagger, enter token directly | If you hit endpoints from client side app, enter 'Bearer [token]'"
                }
            );
            s.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                }
            );
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            s.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
        builder.Services.AddCors(cors =>
        {
            cors.AddPolicy(
                "the.boys.policy",
                options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
            //.SetIsOriginAllowed(origin =>
            //    origin.StartsWith("http://localhost:5173")
            //    || origin == "http://193.227.24.31:5000"
            //    || origin == "http://stage.menofia.edu.eg:5000"
            //    || origin == "https://stage.menofia.edu.eg:5000"
            //)
            //.SetPreflightMaxAge(TimeSpan.FromMinutes(30))
            );
        });
        builder.Services.AddSingleton<IStringLocalizer, JsonStringLocalizer>();
        builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
        builder.Services.Configure<EmailSettings>(
            builder.Configuration.GetSection(nameof(EmailSettings))
        );
        builder.Services.AddSingleton(sp =>
            builder.Configuration.GetSection(nameof(EmailSettings)).Get<EmailSettings>()
        );
        builder.Services.AddScoped<LocalizationMiddleWare>();
        builder.Services.AddScoped<ExceptionHandlingMiddleware>();
        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddScoped<ISeedingService, SeedingService>();
        builder.Services.AddScoped<IUserContext, UserContext>();
        builder.Services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(
                httpContext =>
                {
                    return RateLimitPartition.GetFixedWindowLimiter(
                        $"IP_{httpContext.Connection.RemoteIpAddress}",
                        (ip) =>
                            new FixedWindowRateLimiterOptions
                            {
                                Window = TimeSpan.FromMinutes(1),
                                PermitLimit = 50
                            }
                    );
                }
            );
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        });
        builder
            .Services.RegisterApplicationDependencies()
            .RegisterInfrastructureDependencies(
                builder.Configuration,
                builder.Environment.IsDevelopment()
                    ? EnvironmentType.Development
                    : EnvironmentType.Production
            );
        var app = builder.Build();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseMiddleware<LocalizationMiddleWare>();
        #region seeding
        using (var scope = app.Services.CreateScope())
        {
            var seedingService = scope.ServiceProvider.GetRequiredService<ISeedingService>();
            seedingService.SeedLanguages();
        }
        #endregion
        app.UseCors("the.boys.policy");
        app.UseStaticFiles();
        app.UseRouting();
        app.UseHttpsRedirection();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DisplayRequestDuration();
                c.EnableFilter();
                c.EnablePersistAuthorization();
                c.SwaggerEndpoint("/swagger/the.boys.api/swagger.json", "the.boys.api");
            });
        }
        app.MapControllers();
        app.MapFallbackToFile("index.html");
        app.UseAuthentication().UseAuthorization();
        app.UseMiddleware<LocalizationMiddleWare>();
        app.Run();
    }
}
