using Infrastructure.Data;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    private string messageException = "Null entity reference";

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public async Task<T> CreateAsync(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(messageException);
       
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public T Delete(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(messageException);

        _context.Set<T>().Remove(entity);
        return entity;
    }

    public T Update(T entity)
    {
        if(entity is null) 
            throw new ArgumentNullException(messageException);

        _context.Set<T>().Update(entity);
        return entity;
    }
}
