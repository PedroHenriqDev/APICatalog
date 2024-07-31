namespace Catalog.Application.Interfaces;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    void Commit();
    Task CommitAsync();
}
