using ShopCore.API.DTOS.Products;
using ShopCore.Application.Services.Products;

namespace ShopCore.Application.Interfaces.Products;

public class ProductService : IProductService
{
    public Task<ProductDto?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddAsync(ProductDto product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(ProductDto product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SoftDeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}