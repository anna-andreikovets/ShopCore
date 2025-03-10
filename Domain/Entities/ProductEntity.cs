namespace ShopCore.Domain.Entities;

public class ProductEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public DateTime? DeleteDate { get; set; }
}