using ShopCore.API.DTOS.Orders;

namespace ShopCore.Application.Services.Orders;

public interface IOrderService
{
    Task<bool> CreateOrderAsync(OrderDto request);

    Task<bool> CancelOrderAsync(int orderId);
}