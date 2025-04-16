using Infrastructure.Data.Repositories;

namespace Infrastructure.Data;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }
    Task SaveChangesAsync();
}
