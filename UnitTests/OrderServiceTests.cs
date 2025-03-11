using Microsoft.EntityFrameworkCore;
using Moq;
using ShopCore.API.DTOS.Orders;
using ShopCore.API.DTOS.Products;
using ShopCore.Application.Interfaces.Products;
using ShopCore.Application.Services.Orders;
using ShopCore.Domain.Entities;
using ShopCore.Domain.Enums;
using ShopCore.Infrastructure.Extensions;
using Xunit;

namespace ShopCore.UnitTests;

public class OrderServiceTests
{
    private readonly Mock<ApplicationContext> _contextMock;
    private readonly Mock<IProductService> _productServiceMock;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _contextMock = new Mock<ApplicationContext>();
        _productServiceMock = new Mock<IProductService>();
        _orderService = new OrderService(_contextMock.Object, _productServiceMock.Object);
    }

    [Fact]
    public async Task CreateOrderAsync_ShouldReturnTrue_WhenProductExistsAndStockIsSufficient()
    {
        var product = new ProductEntity { Id = 1, Stock = 10 };
        var orderDto = new OrderDto { ProductId = 1, Quantity = 2 };

        _productServiceMock.Setup(service => service.GetByIdAsync(orderDto.ProductId)).ReturnsAsync(new ProductDto(product));
        var mockDbSet = new Mock<DbSet<OrderEntity>>();
        _contextMock.Setup(context => context.Orders).Returns(mockDbSet.Object);
        
        var result = await _orderService.CreateOrderAsync(orderDto);
        
        Assert.True(result);
        _productServiceMock.Verify(service => service.GetByIdAsync(orderDto.ProductId), Times.Once);
        mockDbSet.Verify(db => db.AddAsync(It.IsAny<OrderEntity>(), default), Times.Once);
        _contextMock.Verify(context => context.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task CreateOrderAsync_ShouldReturnFalse_WhenProductDoesNotExist()
    {
        var orderDto = new OrderDto { ProductId = 1, Quantity = 2 };

        _productServiceMock.Setup(service => service.GetByIdAsync(orderDto.ProductId)).ReturnsAsync((ProductDto)null);

        var result = await _orderService.CreateOrderAsync(orderDto);
        
        Assert.False(result);
        _productServiceMock.Verify(service => service.GetByIdAsync(orderDto.ProductId), Times.Once);
        _contextMock.Verify(context => context.Orders.AddAsync(It.IsAny<OrderEntity>(), default), Times.Never);
        _contextMock.Verify(context => context.SaveChangesAsync(default), Times.Never);
    }

    [Fact]
    public async Task CreateOrderAsync_ShouldReturnFalse_WhenStockIsInsufficient()
    {
        var product = new ProductEntity { Id = 1, Stock = 1 };
        var orderDto = new OrderDto { ProductId = 1, Quantity = 2 };

        _productServiceMock.Setup(service => service.GetByIdAsync(orderDto.ProductId)).ReturnsAsync(new ProductDto(product));
        
        var result = await _orderService.CreateOrderAsync(orderDto);
        
        Assert.False(result);
        _productServiceMock.Verify(service => service.GetByIdAsync(orderDto.ProductId), Times.Once);
        _contextMock.Verify(context => context.Orders.AddAsync(It.IsAny<OrderEntity>(), default), Times.Never);
        _contextMock.Verify(context => context.SaveChangesAsync(default), Times.Never);
    }

    [Fact]
    public async Task CancelOrderAsync_ShouldReturnTrue_WhenOrderExists()
    {
        var order = new OrderEntity { Id = 1, Status = OrderStatusEnum.Created };
        var mockDbSet = new Mock<DbSet<OrderEntity>>();
        mockDbSet.Setup(db => db.FindAsync(1)).ReturnsAsync(order);
        _contextMock.Setup(context => context.Orders).Returns(mockDbSet.Object);
        
        var result = await _orderService.CancelOrderAsync(1);
        
        Assert.True(result);
        Assert.Equal(OrderStatusEnum.Cancelled, order.Status);
        _contextMock.Verify(context => context.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task CancelOrderAsync_ShouldReturnFalse_WhenOrderDoesNotExist()
    {
        var mockDbSet = new Mock<DbSet<OrderEntity>>();
        mockDbSet.Setup(db => db.FindAsync(1)).ReturnsAsync((OrderEntity)null);
        _contextMock.Setup(context => context.Orders).Returns(mockDbSet.Object);
        
        var result = await _orderService.CancelOrderAsync(1);
        
        Assert.False(result);
        _contextMock.Verify(context => context.SaveChangesAsync(default), Times.Never);
    }
}