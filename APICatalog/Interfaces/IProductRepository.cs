using APICatalog.Domain;
using APICatalog.Pagination;

namespace APICatalog.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<PagedList<Product>> GetProductsFilterPriceAsync(ProductsFilterPriceParameters productsParams);
    Task<PagedList<Product>> GetProductsAsync(ProductsParameters productsParams);
    Task<Product> GetByIdWithCategoryAsync(int id);
    Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);
}
