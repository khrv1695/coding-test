using Infrastructure.Data.Repositories;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IProductRepository ProductRepository { get; }

    public UnitOfWork(ApplicationDbContext context, IProductRepository productRepository)
    {
        _context = context;
        ProductRepository = productRepository;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
