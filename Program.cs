using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using ShopCore.Application.Interfaces.Orders;
using ShopCore.Application.Interfaces.Products;
using ShopCore.Application.Services.Orders;
using ShopCore.Application.Services.Products;
using ShopCore.Infrastructure.Extensions;
using ShopCore.Infrastructure.Migrations.Orders;
using ShopCore.Infrastructure.Migrations.Products;

var builder = WebApplication.CreateBuilder(args);

// Регистрация контекста базы данных с использованием Npgsql (PostgreSQL)
builder.Services.AddDbContext<ApplicationContext>((serviceProvider, options) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString);
});

// Настройка FluentMigrator для PostgreSQL
builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(rb =>
    {
        rb
            .AddPostgres()
            .WithGlobalConnectionString(builder.Configuration.GetConnectionString("DefaultConnection"))
            .ScanIn(typeof(Products_2025_03_10_1656).Assembly).For.Migrations()
            .ScanIn(typeof(Orders_2025_03_10_1650).Assembly).For.Migrations();
    })
    .AddLogging(lb => lb.AddConsole());

// Регистрация сервисов
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Добавление поддержки контроллеров
builder.Services.AddControllers();

// Добавление Swagger для документирования API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Применяем миграции при запуске
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    context.Database.Migrate();
    
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

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