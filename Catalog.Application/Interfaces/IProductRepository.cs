using Catalog.Application.Pagination;
using Catalog.Domain;

namespace Catalog.Application.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<PagedList<Product>> GetProductsFilterPriceAsync(ProductsFilterPriceParameters productsParams);
    Task<PagedList<Product>> GetProductsAsync(ProductsParameters productsParams);
    Task<Product?> GetByIdWithCategoryAsync(int id);
    Task<PagedList<Product>> GetByCategoryIdAsync(int categoryId, ProductsParameters productParams);
}
