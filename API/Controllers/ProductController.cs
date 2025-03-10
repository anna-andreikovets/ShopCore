using Microsoft.AspNetCore.Mvc;
using ShopCore.API.DTOS.Products;
using ShopCore.Application.Services.Products;

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
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    // Создание продукта
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductDto request)
    {
        await _productService.AddAsync(request);
        return Ok();
    }

    // Обновление продукта
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto request)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null)
            return NotFound();

        await _productService.UpdateAsync(request);
        return Ok();
    }

    // Логическое удаление продукта
    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDeleteProduct(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null)
            return NotFound();

        await _productService.SoftDeleteAsync(id);
        return Ok();
    }

}