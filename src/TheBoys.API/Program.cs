using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

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
