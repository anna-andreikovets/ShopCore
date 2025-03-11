using Microsoft.EntityFrameworkCore;
using ShopCore.API.DTOS.Products;
using ShopCore.Application.Interfaces.Products;
using ShopCore.Domain.Entities;
using ShopCore.Infrastructure.Extensions;

namespace ShopCore.Application.Services.Products;

public class ProductService : IProductService
{
    readonly ApplicationContext _context;

    public ProductService(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var product = await _context.Products
            .Where(p => p.Id == id)
            .Select(p => new ProductDto(p))
            .FirstOrDefaultAsync();

        return product;
    }

    public async Task AddAsync(NewProductDto product)
    {
        var newProduct = new ProductEntity(product);
        
        await _context.Products.AddAsync(newProduct);
        
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(ProductDto product)
    {
        var oldProduct = _context.Products.FirstOrDefault(p => p.Id == product.Id);

        if (oldProduct == null)
            return false;
        
        oldProduct.Name = product.Name;
        oldProduct.Description = product.Description;
        oldProduct.Price = product.Price;
        oldProduct.Stock = product.Stock;
        oldProduct.DeleteDate = product.DeleteDate;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        
        if(product == null)
            return false;
        
        product.DeleteDate = DateTime.Now;
        
        await _context.SaveChangesAsync();
        
        return true;
    }
}