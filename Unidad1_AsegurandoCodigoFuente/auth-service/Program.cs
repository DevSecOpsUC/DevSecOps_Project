using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/api/auth/login", async (HttpContext context) =>
{
    var body = await JsonSerializer.DeserializeAsync<LoginRequest>(context.Request.Body);
    if (body is not null && body.Username == "admin" && body.Password == "1234")
    {
        await context.Response.WriteAsJsonAsync(new { token = "fake-jwt-token-12345", user = "admin" });
    }
    else
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsJsonAsync(new { error = "Credenciales inválidas" });
    }
});

app.Urls.Add("http://0.0.0.0:80"); // 🔹 línea crucial
app.Run();

record LoginRequest(string Username, string Password);
