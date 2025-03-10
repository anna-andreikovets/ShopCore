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

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDto request)
    {
        await _orderService.CreateOrderAsync(request.ProductId, request.Quantity);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelOrder(int id)
    {
        await _orderService.CancelOrderAsync(id);
        return Ok();
    }
}