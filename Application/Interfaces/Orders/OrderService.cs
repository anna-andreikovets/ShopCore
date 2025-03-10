using Microsoft.EntityFrameworkCore;
using ShopCore.API.DTOS.Orders;
using ShopCore.Application.Services.Orders;
using ShopCore.Domain.Entities;
using ShopCore.Domain.Enums;
using ShopCore.Infrastructure.Extensions;

namespace ShopCore.Application.Interfaces.Orders;

public class OrderService : IOrderService
{
    readonly ApplicationContext _context;

    public OrderService(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task CreateOrderAsync(OrderDto request)
    {
        var order = new OrderEntity(request)
        {
            Status = OrderStatusEnum.Created,
            CreatedDate = DateTime.Now
        };
        
        await _context.Orders.AddAsync(order);

        await _context.SaveChangesAsync();
    }

    public async Task<bool> CancelOrderAsync(int orderId)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
            return false;
        
        order.Status = OrderStatusEnum.Cancelled;

        await _context.SaveChangesAsync();
        
        return true;
    }
}