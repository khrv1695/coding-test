using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product> GetProductAsync(Guid id)
    {
        return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _context.Products.AsNoTracking().ToListAsync();
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
