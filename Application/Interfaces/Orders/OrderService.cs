using ShopCore.Application.Services.Orders;

namespace ShopCore.Application.Interfaces.Orders;

public class OrderService : IOrderService
{
    public Task CreateOrderAsync(int productId, int quantity)
    {
        throw new NotImplementedException();
    }

    public Task CancelOrderAsync(int orderId)
    {
        throw new NotImplementedException();
    }
}