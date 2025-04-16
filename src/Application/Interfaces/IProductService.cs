using Application.DTOs;

namespace Application.Interfaces;

public interface IProductService
{
    Task<ProductDto> GetProductAsync(Guid id);
    Task<IEnumerable<ProductDto>> GetProductsAsync();
    Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto);
    Task UpdateProductAsync(UpdateProductDto updateProductDto);
    Task DeleteProductAsync(Guid id);
}
