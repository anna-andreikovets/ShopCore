using ShopCore.API.DTOS.Orders;

namespace ShopCore.Application.Interfaces.Orders;

public interface IOrderService
{
    Task CreateOrderAsync(OrderDto request);

    Task<bool> CancelOrderAsync(int orderId);
}