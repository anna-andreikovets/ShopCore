using Microsoft.EntityFrameworkCore;
using Moq;
using ShopCore.API.DTOS.Products;
using ShopCore.Application.Services.Products;
using ShopCore.Domain.Entities;
using ShopCore.Infrastructure.Extensions;
using Xunit;

namespace ShopCore.UnitTests;

public class ProductServiceTests
{
    private readonly Mock<ApplicationContext> _contextMock;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _contextMock = new Mock<ApplicationContext>();
        _productService = new ProductService(_contextMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProduct_WhenProductExists()
    {
        // Arrange
        var productId = 1;
        var productEntity = new ProductEntity
        {
            Id = productId,
            Name = "Product1",
            Description = "Description1",
            Price = 10.0m,
            Stock = 100
        };

        var mockDbSet = new Mock<DbSet<ProductEntity>>();
        mockDbSet.Setup(db => db.FindAsync(productId)).ReturnsAsync(productEntity);
        _contextMock.Setup(context => context.Products).Returns(mockDbSet.Object);

        // Act
        var result = await _productService.GetByIdAsync(productId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productId, result?.Id);
        Assert.Equal("Product1", result?.Name);
        Assert.Equal("Description1", result?.Description);
    }

    [Fact]
    public async Task AddAsync_ShouldAddProductSuccessfully()
    {
        // Arrange
        var newProductDto = new NewProductDto
        {
            Name = "New Product",
            Description = "New Description",
            Price = 15.0m,
            Stock = 50
        };

        var mockDbSet = new Mock<DbSet<ProductEntity>>();
        _contextMock.Setup(context => context.Products).Returns(mockDbSet.Object);

        // Act
        await _productService.AddAsync(newProductDto);

        // Assert
        mockDbSet.Verify(db => db.AddAsync(It.IsAny<ProductEntity>(), default), Times.Once);
        _contextMock.Verify(context => context.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnTrue_WhenProductIsUpdated()
    {
        // Arrange
        var productDto = new UpdateProductDto
        {
            Id = 1,
            Name = "Updated Product",
            Description = "Updated Description",
            Price = 20.0m,
            Stock = 200
        };

        var productEntity = new ProductEntity
        {
            Id = productDto.Id,
            Name = "Old Product",
            Description = "Old Description",
            Price = 10.0m,
            Stock = 100
        };

        var mockDbSet = new Mock<DbSet<ProductEntity>>();
        mockDbSet.Setup(db => db.FindAsync(productDto.Id)).ReturnsAsync(productEntity);
        _contextMock.Setup(context => context.Products).Returns(mockDbSet.Object);

        // Act
        var result = await _productService.UpdateAsync(productDto);

        // Assert
        Assert.True(result);
        Assert.Equal(productDto.Name, productEntity.Name);
        Assert.Equal(productDto.Description, productEntity.Description);
        Assert.Equal(productDto.Price, productEntity.Price);
        Assert.Equal(productDto.Stock, productEntity.Stock);
        _contextMock.Verify(context => context.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnFalse_WhenProductDoesNotExist()
    {
        // Arrange
        var productDto = new UpdateProductDto
        {
            Id = 999,
            Name = "Updated Product",
            Description = "Updated Description",
            Price = 20.0m,
            Stock = 200
        };

        var mockDbSet = new Mock<DbSet<ProductEntity>>();
        mockDbSet.Setup(db => db.FindAsync(productDto.Id)).ReturnsAsync((ProductEntity)null);
        _contextMock.Setup(context => context.Products).Returns(mockDbSet.Object);

        // Act
        var result = await _productService.UpdateAsync(productDto);

        // Assert
        Assert.False(result);
        _contextMock.Verify(context => context.SaveChangesAsync(default), Times.Never);
    }

    [Fact]
    public async Task SoftDeleteAsync_ShouldReturnTrue_WhenProductIsSoftDeleted()
    {
        // Arrange
        var productId = 1;
        var productEntity = new ProductEntity
        {
            Id = productId,
            Name = "Product1",
            Description = "Description1",
            Price = 10.0m,
            Stock = 100
        };

        var mockDbSet = new Mock<DbSet<ProductEntity>>();
        mockDbSet.Setup(db => db.FindAsync(productId)).ReturnsAsync(productEntity);
        _contextMock.Setup(context => context.Products).Returns(mockDbSet.Object);

        // Act
        var result = await _productService.SoftDeleteAsync(productId);

        // Assert
        Assert.True(result);
        Assert.NotNull(productEntity.DeleteDate);
        _contextMock.Verify(context => context.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task SoftDeleteAsync_ShouldReturnFalse_WhenProductDoesNotExist()
    {
        // Arrange
        var productId = 999;

        var mockDbSet = new Mock<DbSet<ProductEntity>>();
        mockDbSet.Setup(db => db.FindAsync(productId)).ReturnsAsync((ProductEntity)null);
        _contextMock.Setup(context => context.Products).Returns(mockDbSet.Object);

        // Act
        var result = await _productService.SoftDeleteAsync(productId);

        // Assert
        Assert.False(result);
        _contextMock.Verify(context => context.SaveChangesAsync(default), Times.Never);
    }
}