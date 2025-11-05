using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Configurar CORS para permitir solicitudes desde el frontend en Azure
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins("https://frontend-web-devsecopsuc-gjaqc5bkaabpazeh.eastus2-01.azurewebsites.net")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();
app.UseCors();

// 🔹 Endpoint principal: listado de habitaciones con imágenes y precios
app.MapGet("/api/rooms", () =>
{
    var rooms = new[]
    {
        new
        {
            name = "Habitación 101",
            price = 680000,
            image = "https://casallasduque.com/wp-content/uploads/2022/07/remodelacion-apartamentos-montearrollo-habitacion-principal.jpg"
        },
        new
        {
            name = "Habitación 202",
            price = 550000,
            image = "https://www.mobiprix.com/cdnassets/products/3311/space/118365_s.webp"
        },
        new
        {
            name = "Habitación 303",
            price = 750000,
            image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTTD-uZbAICjNy10pTQaHGjGOVUrwmmIRRmCg&s"
        },
        new
        {
            name = "Habitación 404",
            price = 600000,
            image = "https://images.sodimac.com/v3/assets/blt2f8082df109cfbfb/bltd6119d8ee677aac1/6617ecc2569f272218aee1e0/LND-GC-451-PC1-top-mejores-tipos-de-decoracion.jpg"
        },
        new
        {
            name = "Habitación 505",
            price = 820000,
            image = "https://images.sodimac.com/v3/assets/blt2f8082df109cfbfb/blt2a75777fe441f185/65402bcda184e7001b4f867c/LND-GC-917-PC-habitacion-con-pared-de-madera-mb.jpg"
        },
        new
        {
            name = "Habitación 606",
            price = 700000,
            image = "https://cf.bstatic.com/xdata/images/hotel/max1024x768/575649960.jpg?k=f18bc7e3aa29898320bbe9f46e5d9da25c643060a4777805cefab6b2bf8363c2&o="
        },
        new
        {
            name = "Habitación 707",
            price = 890000,
            image = "https://www.principado.com.ar/assets/cache/uploads/fotos-principados-2022/1920x1080/interior-habitacion-cama-vista-frontal-principado-downtown-buenos-aires-hotel-argentina.jpg"
        },
        new
        {
            name = "Habitación 808",
            price = 650000,
            image = "https://img.freepik.com/foto-gratis/disposiciones-camas-naturaleza-muerta_23-2150532993.jpg?semt=ais_hybrid&w=740&q=80"
        },
        new
        {
            name = "Habitación 909",
            price = 930000,
            image = "https://image-tc.galaxy.tf/wijpeg-5srw8mjtza6abfwrpha1r5lmg/habitacion-special-double_standard.jpg?crop=107%2C0%2C1707%2C1280"
        }
    };

    return Results.Json(rooms);
});

// 🔹 Configurar el puerto HTTP
app.Urls.Add("https://0.0.0.0:80");

app.Run();
