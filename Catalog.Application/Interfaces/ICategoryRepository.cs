using Catalog.Domain;
using Catalog.Application.Pagination;

namespace Catalog.Application.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
   Task<PagedList<Category>> GetCategoriesFilterNameAsync(CategoriesFilterNameParameters categoriesFilterNameParams);
   Task<PagedList<Category>> GetCategoriesAsync(CategoriesParameters categoriesParams);
   Task<PagedList<Category>> GetCategoriesWithProductsAsync(CategoriesParameters categoriesParams);
   Task<Category> GetByIdWithProductsAsync(int id);
   Task<Category> GetCategoryByNameAsync(string name);
}
