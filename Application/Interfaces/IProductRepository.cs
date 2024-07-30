using Application.Pagination;
using Infrastructure.Domain;

namespace Application.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<PagedList<Product>> GetProductsFilterPriceAsync(ProductsFilterPriceParameters productsParams);
    Task<PagedList<Product>> GetProductsAsync(ProductsParameters productsParams);
    Task<Product?> GetByIdWithCategoryAsync(int id);
    Task<PagedList<Product>> GetByCategoryIdAsync(int categoryId, ProductsParameters productParams);
}
