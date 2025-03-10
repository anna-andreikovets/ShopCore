namespace ShopCore.Application.Services.Orders;

public interface IOrderService
{
    Task CreateOrderAsync(int productId, int quantity);

    Task CancelOrderAsync(int orderId);
}