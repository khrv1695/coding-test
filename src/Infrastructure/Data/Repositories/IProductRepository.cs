using Domain.Entities;

namespace Infrastructure.Data.Repositories;

public interface IProductRepository
{
    Task<Product> GetProductAsync(Guid id);
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product> CreateProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(Guid id);
}
