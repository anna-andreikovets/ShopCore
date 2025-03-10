using Moq;
using ShopCore.API.DTOS.Orders;
using ShopCore.Application.Services.Orders;
using Xunit;

namespace ShopCore.UnitTests;

public class OrderServiceTests
{
    private readonly Mock<IOrderService> _orderServiceMock;
    
    public OrderServiceTests()
    {
        _orderServiceMock = new Mock<IOrderService>();
    }

    [Fact]
    public async Task CreateOrderAsync_ShouldCallCreateOrderOnce()
    {
        var orderDto = new OrderDto
        {
            ProductId = 1,
            Quantity = 2,
        };
        
        await _orderServiceMock.Object.CreateOrderAsync(orderDto);
        
        _orderServiceMock.Verify(service => service.CreateOrderAsync(orderDto), Times.Once);
    }

    [Fact]
    public async Task CancelOrderAsync_ShouldReturnTrue_WhenOrderExists()
    {
        int orderId = 1;
        _orderServiceMock.Setup(service => service.CancelOrderAsync(orderId)).ReturnsAsync(true);
        
        var result = await _orderServiceMock.Object.CancelOrderAsync(orderId);
        
        Assert.True(result);
    }

    [Fact]
    public async Task CancelOrderAsync_ShouldReturnFalse_WhenOrderDoesNotExist()
    {
        int orderId = 2;
        _orderServiceMock.Setup(service => service.CancelOrderAsync(orderId)).ReturnsAsync(false);
        
        var result = await _orderServiceMock.Object.CancelOrderAsync(orderId);
        
        Assert.False(result);
    }
}