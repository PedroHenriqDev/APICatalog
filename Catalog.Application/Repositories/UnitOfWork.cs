using Catalog.Infrastructure.Data;
using Catalog.Application.Interfaces;

namespace Catalog.Application.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public AppDbContext _context;
    private IProductRepository? _productRepository;
    private ICategoryRepository? _categoryRepository;

    public UnitOfWork(AppDbContext context) 
    {
        _context = context;
    }

    public IProductRepository ProductRepository 
    {
        get 
        {
            return _productRepository = _productRepository ?? new ProductRepository(_context);
        }
    }

    public ICategoryRepository CategoryRepository
    {
        get
        {
            return _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);
        }
    }

    public void Dispose() 
    {
        _context.Dispose();
    }

    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    public void Commit()
    {
        _context.SaveChanges();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}
