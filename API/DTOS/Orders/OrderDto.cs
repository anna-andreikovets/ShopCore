using ShopCore.Domain.Entities;
using ShopCore.Domain.Enums;

namespace ShopCore.API.DTOS.Orders;

public class OrderDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}