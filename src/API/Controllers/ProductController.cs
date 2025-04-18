using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    // [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMediator _mediator;
        private readonly ILogger<ProductController> _logger;

        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            return Ok();
        }

        public ProductController(IProductService productService, IMediator mediator, ILogger<ProductController> logger)
        {
            _productService = productService;
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            _logger.LogInformation("Getting product with id: {id}", id);
            var product = await _productService.GetProductAsync(id);
            if (product == null)
            {
                _logger.LogWarning("Product with id: {id} not found", id);
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            _logger.LogInformation("Getting all products");
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            _logger.LogInformation("Creating product");
            var command = new CreateProductCommand(createProductDto);
            var product = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto updateProductDto)
        {
            _logger.LogInformation("Updating product");
            await _productService.UpdateProductAsync(updateProductDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            _logger.LogInformation("Deleting product with id: {id}", id);
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
