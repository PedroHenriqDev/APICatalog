using APICatalog.Context;
using APICatalog.Domain;
using APICatalog.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Category> GetByIdWithProductsAsync(int id)
        {
            return await _context.Categories.AsNoTracking()
                                            .Include(category => category.Products)
                                            .FirstOrDefaultAsync(category => category.CategoryId == id);
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            return await _context.Categories.AsNoTracking()
                                            .FirstOrDefaultAsync(category => category.Name == name);
        }

        public async Task<IEnumerable<Category>> GetAllWithProductsAsync()
        {
            return await _context.Categories.AsNoTracking()
                                            .Include(category => category.Products)
                                            .ToListAsync();
        }
    }
}
