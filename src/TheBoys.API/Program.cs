using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TheBoys.API.Data;
using TheBoys.API.ExternalServices.Email;
using TheBoys.API.Services.News;
using TheBoys.API.Settings;

namespace TheBoys.API;

public class Program
{
    public static void Main(string[] args)
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
        builder.Services.AddCors(cfg =>
            cfg.AddPolicy(
                "the.boys.policy",
                cfg => cfg.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
            )
        );

        builder.Services.AddDbContext<ApplicationDbContext>(cfg =>
            cfg.UseSqlServer(
                builder.Configuration.GetConnectionString(
                    builder.Environment.IsDevelopment()
                        ? "LocalDatabaseConnection"
                        : "ProductionDatabaseConnection"
                )
            )
        );
        builder.Services.Configure<EmailSettings>(
            builder.Configuration.GetSection("EmailSettings")
        );
        builder.Services.AddSingleton(sp =>
            builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>()
        );
        builder.Services.AddScoped<INewsDaoService, NewsDaoService>();
        builder.Services.AddScoped<IEmailService, EmailService>();
        var app = builder.Build();
        app.UseCors("the.boys.policy");
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/the.boys.api/swagger.json", "the.boys.api");
            c.DisplayRequestDuration();
            c.EnableFilter();
            c.EnablePersistAuthorization();
        });
        app.MapControllers();
        app.UseStaticFiles();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
