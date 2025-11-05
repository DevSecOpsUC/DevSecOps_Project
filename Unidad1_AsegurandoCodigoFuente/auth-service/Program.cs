using Common; // <-- Importa la librería compartida
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

namespace AuthService
{
    public partial class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 🔹 Configurar servicios comunes (Swagger, etc.)
            builder.Services.AddCommonServices();

            // 🔹 Configurar CORS para permitir tu frontend en Azure
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                    policy.WithOrigins("https://frontend-web-devsecopsuc-gjaqc5bkaabpazeh.eastus2-01.azurewebsites.net")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
            });

            var app = builder.Build();

            // 🔹 Pipeline común (HSTS, HTTPS, Swagger, etc.)
            app.UseCommonPipeline();
            app.UseCors();

            // 🔹 Endpoint de autenticación
            app.MapPost("/api/auth/login", async (HttpContext context) =>
            {
                var body = await context.Request.ReadFromJsonAsync<LoginRequest>();

                if (body is null ||
                    string.IsNullOrWhiteSpace(body.Username) ||
                    string.IsNullOrWhiteSpace(body.Password))
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid credentials");
                    return;
                }

                if (body.Username.Equals("username", StringComparison.OrdinalIgnoreCase) &&
                    body.Password.Equals("password", StringComparison.OrdinalIgnoreCase))
                {
                    await context.Response.WriteAsJsonAsync(new { token = "token_123", user = body.Username });
                }
                else
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized");
                }
            });

            // 🔹 Endpoint de prueba (para cobertura de test)
            app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

            app.Urls.Add("https://0.0.0.0:80");

            await app.RunAsync();
        }

        private sealed record LoginRequest(string Username, string Password);
    }
}
