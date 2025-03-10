using ShopCore.Domain.Entities;

namespace ShopCore.API.DTOS.Products;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public DateTime? DeleteDate { get; set; }

    public ProductDto(ProductEntity product)
    {
        Id = product.Id;
        Name = product.Name;
        Description = product.Description;
        Price = product.Price;
        Stock = product.Stock;
        DeleteDate = product.DeleteDate;
    }
}