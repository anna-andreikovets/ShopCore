using Microsoft.EntityFrameworkCore;
using ShopCore.Domain.Entities;

namespace ShopCore.Infrastructure.Extensions;

public class ApplicationContext : DbContext
{
    private readonly IServiceProvider _serviceProvider;
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options, IServiceProvider serviceProvider)
        : base(options)
    {
        _serviceProvider = serviceProvider;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Database.EnsureCreated();
        
        var connectionString = "Server=localhost;Database=ShopDB;User Id=sa;Password=P@ssw0rd123;";
        optionsBuilder.UseSqlServer(connectionString);
    }
    
    public DbSet<OrderEntity> Orders { get; set; }
    
    public DbSet<ProductEntity> Products { get; set; }
}