using ShopCore.API.DTOS.Products;

namespace ShopCore.Application.Services.Products;

public interface IProductService
{
    Task<ProductDto?> GetByIdAsync(int id);
    Task AddAsync(NewProductDto product);
    Task<bool> UpdateAsync(ProductDto product);
    Task<bool> SoftDeleteAsync(int id);
}