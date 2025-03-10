using ShopCore.API.DTOS.Products;

namespace ShopCore.Application.Services.Products;

public interface IProductService
{
    Task<ProductDto> GetByIdAsync(int id);
    Task AddAsync(ProductDto product);
    Task UpdateAsync(ProductDto product);
    Task SoftDeleteAsync(int id);
}