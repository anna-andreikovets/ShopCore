using ShopCore.API.DTOS.Products;

namespace ShopCore.Domain.Entities;

public class ProductEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public DateTime? DeleteDate { get; set; }

    public ProductEntity()
    {
    }
    
    public ProductEntity(NewProductDto product)
    {
        Name = product.Name;
        Description = product.Description;
        Price = product.Price;
        Stock = product.Stock;
        DeleteDate = DateTime.Now;
    }
}