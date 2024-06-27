using APICatalog.Context;
using APICatalog.Domain;
using APICatalog.Interfaces;
using APICatalog.Pagination;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) 
    {
    }

    public async Task<PagedList<Product>> GetProductsAsync(ProductsParameters productsParams)
    {
        var products = await GetAllAsync();

        var productsOrdered = PagedList<Product>.ToPagedList(products.AsQueryable(),
                                                             productsParams.PageNumber,
                                                             productsParams.PageSize);

        return productsOrdered;
    }

    public async Task<PagedList<Product>> GetProductsFilterPriceAsync(ProductsFilterPriceParameters productsParams)
    {
        var products = await GetAllAsync();

        if(productsParams.Price.HasValue && !string.IsNullOrEmpty(productsParams.PriceParameter)) 
        {
            if(productsParams.PriceParameter.Equals("bigger", StringComparison.OrdinalIgnoreCase))
            {
                products = products.Where(product => product.Price > productsParams.Price)
                    .OrderBy(product => product.Price);
            }
            else if(productsParams.PriceParameter.Equals("equal", StringComparison.OrdinalIgnoreCase))
            {
                products = products.Where(product => product.Price ==  productsParams.Price)
                    .OrderBy(product => product.Price);
            }
            else if (productsParams.PriceParameter.Equals("smaller")) 
            {
                products = products.Where(product => product.Price < productsParams.Price)
                    .OrderBy(product => product.Price);
            }
        }

        return PagedList<Product>.ToPagedList(products.AsQueryable(),
                                              productsParams.PageNumber,
                                              productsParams.PageSize);
    }

    public async Task<Product> GetByIdWithCategoryAsync(int id)
    {
        return await _context.Products.AsNoTracking()
                                      .Include(product => product.Category)
                                      .FirstOrDefaultAsync(product => product.ProductId == id);
    }

    public async Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId)
    {
        return await _context.Products.AsNoTracking()
                                      .Include(product => product.Category)
                                      .Where(product => product.CategoryId == categoryId)
                                      .ToListAsync();
    }
}
