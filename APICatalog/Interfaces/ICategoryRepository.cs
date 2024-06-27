using APICatalog.Domain;
using APICatalog.Pagination;

namespace APICatalog.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
   Task<PagedList<Category>> GetCategoriesFilterNameAsync(CategoriesFilterNameParameters categoriesFilterNameParams);
   Task<PagedList<Category>> GetCategoriesAsync(CategoriesParameters categoriesParams);
   Task<Category> GetByIdWithProductsAsync(int id);
   Task<Category> GetCategoryByNameAsync(string name);
}
