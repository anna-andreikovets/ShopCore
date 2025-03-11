using Microsoft.EntityFrameworkCore;
using ShopCore.API.DTOS.Orders;
using ShopCore.Application.Interfaces.Orders;
using ShopCore.Application.Interfaces.Products;
using ShopCore.Domain.Entities;
using ShopCore.Domain.Enums;
using ShopCore.Infrastructure.Extensions;

namespace ShopCore.Application.Services.Orders;

public class OrderService : IOrderService
{
    readonly ApplicationContext _context;
    readonly IProductService _productService;
    
    public OrderService(ApplicationContext context, IProductService productService)
    {
        _context = context;
        _productService = productService;
    }
    
    public async Task<bool> CreateOrderAsync(OrderDto request)
    {
        var product = await _productService.GetByIdAsync(request.ProductId);

        if (product == null)
            return false;

        if (product.Stock < request.Quantity || product.DeleteDate != null)
            return false;
        
        var order = new OrderEntity(request)
        {
            Status = OrderStatusEnum.Created,
            CreatedDate = DateTime.UtcNow
        };
        
        await _context.Orders.AddAsync(order);

        await _context.SaveChangesAsync();
        
        return true;
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