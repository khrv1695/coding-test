using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var product = await _productService.GetProductAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetProductsAsync();
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
    {
        var product = await _productService.CreateProductAsync(createProductDto);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto updateProductDto)
    {
        await _productService.UpdateProductAsync(updateProductDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        await _productService.DeleteProductAsync(id);
        return NoContent();
    }
}
