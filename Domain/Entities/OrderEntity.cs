using ShopCore.API.DTOS.Orders;
using ShopCore.Domain.Enums;

namespace ShopCore.Domain.Entities;

public class OrderEntity
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public OrderStatusEnum Status { get; set; }
    public DateTime CreatedDate { get; set; }

    public OrderEntity()
    {
    }
    
    public OrderEntity(OrderDto order)
    {
        ProductId = order.ProductId;
        Quantity = order.Quantity;
    }
}