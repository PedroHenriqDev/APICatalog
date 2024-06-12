using APICatalog.Domain;

namespace APICatalog.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllWithCategoryAsync();
        Task<Product> GetByIdWithCategoryAsync(int id);
        Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);
    }
}
