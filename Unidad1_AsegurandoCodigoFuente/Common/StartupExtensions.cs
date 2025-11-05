using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;       // IsDevelopment()
using Microsoft.OpenApi.Models;           // OpenApiInfo

namespace Common;

public static class StartupExtensions
{
    /// <summary>
    /// Registra servicios comunes (Swagger, Endpoints).
    /// </summary>
    public static void AddCommonServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "DevSecOps API",
                Version = "v1",
                Description = "APIs compartidas del proyecto DevSecOpsUC"
            });
        });
    }

    /// <summary>
    /// Configura el pipeline común (Swagger en dev, HSTS en prod, HTTPS).
    /// </summary>
    public static void UseCommonPipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
    }
}
