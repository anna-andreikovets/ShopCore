using Microsoft.EntityFrameworkCore;
using ShopCore.Application.Interfaces.Orders;
using ShopCore.Application.Interfaces.Products;
using ShopCore.Application.Services.Orders;
using ShopCore.Application.Services.Products;
using ShopCore.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Добавление конфигурации
builder.Configuration.AddJsonFile("appsettings.json");

// Регистрация ApplicationContext в DI
builder.Services.AddDbContext<ApplicationContext>((serviceProvider, options) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("ConnectionString");
    options.UseSqlServer(connectionString);
});

// Регистрация сервисов
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Добавление поддержки контроллеров
builder.Services.AddControllers();

// Добавление Swagger для документирования API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Настройка конвейера запросов
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Маршрутизация контроллеров
app.MapControllers();

app.Run();