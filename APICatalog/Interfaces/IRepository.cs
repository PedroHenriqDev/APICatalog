﻿using System.Linq.Expressions;

namespace APICatalog.Interfaces;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
    Task<T> CreateAsync(T entity);
    T Update(T entity);
    T Delete(T entity);
}
