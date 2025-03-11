using Microsoft.AspNetCore.Mvc;
using ShopCore.API.DTOS.Api;
using ShopCore.API.DTOS.Orders;
using ShopCore.Application.Interfaces.Orders;

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
    public async Task<Response<bool>> CreateOrder([FromBody] OrderDto request)
    {
        var success = await _orderService.CreateOrderAsync(request);

        if (!success)
            return new Response<bool>()
            {
                Success = false,
                Message = "Не удалось создать заказ!",
                Result = success
            };
        
        return new Response<bool>()
        {
            Message = "Заказ был успешно создан",
            Result = success
        };
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