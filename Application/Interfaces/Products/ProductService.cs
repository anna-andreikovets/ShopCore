using ShopCore.API.DTOS.Products;
using ShopCore.Application.Services.Products;

namespace ShopCore.Application.Interfaces.Products;

public class ProductService : IProductService
{
    public Task<ProductDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(ProductDto product)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(ProductDto product)
    {
        throw new NotImplementedException();
    }

    public Task SoftDeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}