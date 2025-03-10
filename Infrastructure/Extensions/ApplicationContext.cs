using Microsoft.EntityFrameworkCore;
using ShopCore.Domain.Entities;

namespace ShopCore.Infrastructure.Extensions;

public class ApplicationContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Получаем строку подключения из конфигурации
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    // DbSet для сущностей
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
}