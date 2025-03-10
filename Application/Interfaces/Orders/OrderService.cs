using ShopCore.API.DTOS.Orders;
using ShopCore.Application.Services.Orders;

namespace ShopCore.Application.Interfaces.Orders;

public class OrderService : IOrderService
{
    public Task<bool> CreateOrderAsync(OrderDto request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CancelOrderAsync(int orderId)
    {
        throw new NotImplementedException();
    }
}