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
        
    }
    
    public DbSet<OrderEntity> Orders { get; set; }
    
    public DbSet<ProductEntity> Products { get; set; }
}