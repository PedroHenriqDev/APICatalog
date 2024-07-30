using Infrastructure.Data;
using Infrastructure.Domain;
using Application.Interfaces;
using Application.Pagination;
using Microsoft.EntityFrameworkCore;
using Application.Mapper;

namespace Application.Repositories;

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
            products = productsParams.PriceParameter.ToLower() switch
            {
                "bigger" => products = products.Where(product => product.Price > productsParams.Price)
                    .OrderBy(product => product.Price),

                "equal" => products = products.Where(product => product.Price == productsParams.Price)
                    .OrderBy(product => product.Price),

                "smaller" => products = products.Where(product => product.Price < productsParams.Price)
                    .OrderBy(product => product.Price),

                _ => products
            };
        }

        return PagedList<Product>.ToPagedList(products.AsQueryable(),
                                              productsParams.PageNumber,
                                              productsParams.PageSize);
    }

    public async Task<Product?> GetByIdWithCategoryAsync(int id)
    {
        var products = await _context.Products
            .AsNoTracking()
            .Include(product => product.Category)
            .FirstOrDefaultAsync(product => product.ProductId == id);

        var completeProduct = products!.MapProductWithCategory();

        return completeProduct;
    }

    public async Task<PagedList<Product>> GetByCategoryIdAsync(int categoryId, ProductsParameters productParams)
    {
        var products = await _context.Products
            .AsNoTracking()
            .Include(product => product.Category)
            .Where(product => product.CategoryId == categoryId)
            .ToListAsync();

        var completeProducts = products.Select(product => product.MapProductWithCategory());

        return PagedList<Product>.ToPagedList(completeProducts.AsQueryable(), productParams.PageNumber, productParams.PageSize);
    }
}
