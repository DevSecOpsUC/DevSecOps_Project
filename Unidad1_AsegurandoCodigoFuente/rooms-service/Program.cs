using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/api/rooms", () =>
{
    var rooms = new[]
    {
        new { Name = "Habitación 101", Price = 680000 },
        new { Name = "Habitación 202", Price = 550000 },
        new { Name = "Habitación 303", Price = 750000 },
        new { Name = "Habitación 404", Price = 600000 }
    };
    return Results.Json(rooms);
});

app.Run();
