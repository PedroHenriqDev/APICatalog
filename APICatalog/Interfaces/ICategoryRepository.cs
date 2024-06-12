using APICatalog.Domain;

namespace APICatalog.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
       Task<IEnumerable<Category>> GetAllWithProductsAsync();
       Task<Category> GetByIdWithProductsAsync(int id);
       Task<Category> GetCategoryByNameAsync(string name);
    }
}
