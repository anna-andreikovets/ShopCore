using Microsoft.AspNetCore.Mvc;
using ShopCore.API.DTOS.Api;
using ShopCore.API.DTOS.Products;
using ShopCore.Application.Interfaces.Products;

namespace ShopCore.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    /// <summary>
    /// Получение продукта
    /// </summary>
    [HttpGet("{id}")]
    public async Task<Response<ProductDto>> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
            return new Response<ProductDto>()
            {
                Success = false,
                Message = "Не удалось добавить продукт!"
            };

        return new Response<ProductDto>()
        {
            Message = "Продукт был успешно добавлен!",
            Result = product
        };
    }

    /// <summary>
    /// Добавление продукта
    /// </summary>
    [HttpPost]
    public async Task<bool> AddProduct([FromBody] NewProductDto request)
    {
        await _productService.AddAsync(request);
        
        return true;
    }

    /// <summary>
    /// Обновление продукта
    /// </summary>
    [HttpPut()]
    public async Task<bool> UpdateProduct([FromBody] ProductDto request)
    {
        var success = await _productService.UpdateAsync(request);
        
        return success;
    }

    /// <summary>
    /// Удаление продукта 
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<bool> SoftDeleteProduct(int id)
    {
        var success = await _productService.SoftDeleteAsync(id);
        
        return success;
    }
}