using Infrastructure.Domain;
using Application.Pagination;

namespace Application.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<PagedList<Product>> GetProductsFilterPriceAsync(ProductsFilterPriceParameters productsParams);
    Task<PagedList<Product>> GetProductsAsync(ProductsParameters productsParams);
    Task<Product?> GetByIdWithCategoryAsync(int id);
    Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);
}
