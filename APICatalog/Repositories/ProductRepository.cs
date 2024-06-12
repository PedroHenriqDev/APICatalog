using APICatalog.Context;
using APICatalog.Domain;
using APICatalog.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) 
        {
        }

        public async Task<IEnumerable<Product>> GetAllWithCategoryAsync()
        {
            return await _context.Products.AsNoTracking()
                                          .Include(product => product.Category)
                                          .ToListAsync();
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
}
