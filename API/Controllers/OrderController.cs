using Microsoft.AspNetCore.Mvc;
using ShopCore.API.DTOS.Orders;
using ShopCore.Application.Services.Orders;

namespace ShopCore.API.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// Создание заказа
    /// </summary>
    [HttpPost]
    public async Task<bool> CreateOrder([FromBody] OrderDto request)
    {
        var success = await _orderService.CreateOrderAsync(request);
        return success;
    }
    
    /// <summary>
    /// Отмена заказа
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<bool> CancelOrder(int id)
    {
        var success = await _orderService.CancelOrderAsync(id);
        return success;
    }
}