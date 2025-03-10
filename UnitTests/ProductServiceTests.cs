using Moq;
using ShopCore.API.DTOS.Products;
using ShopCore.Application.Services.Products;
using ShopCore.Domain.Entities;
using Xunit;

namespace ShopCore.UnitTests;

public class ProductServiceTests
{
    private readonly Mock<IProductService> _productServiceMock;

    public ProductServiceTests()
    {
        _productServiceMock = new Mock<IProductService>();
    }
    
    [Fact]
    public async Task GetByIdAsync_ShouldReturnProduct_WhenProductExists()
    {
        var productId = 1;
        var productEntity = new ProductEntity
        {
            Id = 1,
            Name = "Product1",
            Description = "Description1",
            Price = 10.0m,
            Stock = 100
        };
        var productDto = new ProductDto(productEntity);

        _productServiceMock.Setup(service => service.GetByIdAsync(productId)).ReturnsAsync(productDto);
        
        var result = await _productServiceMock.Object.GetByIdAsync(productId);
        
        Assert.NotNull(result);
        Assert.Equal(productId, result?.Id);
        Assert.Equal("Product1", result?.Name);
        Assert.Equal("Description1", result?.Description);
    }
    
    [Fact]
    public async Task AddAsync_ShouldAddProductSuccessfully()
    {
        var newProductDto = new NewProductDto
        {
            Name = "New Product",
            Description = "New Description",
            Price = 15.0m,
            Stock = 50
        };
        
        await _productServiceMock.Object.AddAsync(newProductDto);
        
        _productServiceMock.Verify(service => service.AddAsync(newProductDto), Times.Once);
    }
    
    [Fact]
    public async Task UpdateAsync_ShouldReturnTrue_WhenProductIsUpdated()
    {
        var productDto = new ProductDto(new ProductEntity
        {
            Id = 1,
            Name = "Updated Product",
            Description = "Updated Description",
            Price = 20.0m,
            Stock = 200
        });

        _productServiceMock.Setup(service => service.UpdateAsync(productDto)).ReturnsAsync(true);
        
        var result = await _productServiceMock.Object.UpdateAsync(productDto);
        
        Assert.True(result);
        _productServiceMock.Verify(service => service.UpdateAsync(productDto), Times.Once);
    }
    
    [Fact]
    public async Task SoftDeleteAsync_ShouldReturnTrue_WhenProductIsSoftDeleted()
    {
        int productId = 1;
        _productServiceMock.Setup(service => service.SoftDeleteAsync(productId)).ReturnsAsync(true);
        
        var result = await _productServiceMock.Object.SoftDeleteAsync(productId);
        
        Assert.True(result);
        _productServiceMock.Verify(service => service.SoftDeleteAsync(productId), Times.Once);
    }
    
    [Fact]
    public async Task SoftDeleteAsync_ShouldReturnFalse_WhenProductDoesNotExist()
    {
        int productId = 999;
        _productServiceMock.Setup(service => service.SoftDeleteAsync(productId)).ReturnsAsync(false);
        
        var result = await _productServiceMock.Object.SoftDeleteAsync(productId);
        
        Assert.False(result);
        _productServiceMock.Verify(service => service.SoftDeleteAsync(productId), Times.Once);
    }
}
