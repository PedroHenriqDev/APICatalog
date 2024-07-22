using Infrastructure.Data;
using Infrastructure.Domain;
using Application.Interfaces;
using Application.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<PagedList<Category>> GetCategoriesAsync(CategoriesParameters categoriesParams)
    {
        var categories = await GetAllAsync();

        var categoriesOrdered = categories.OrderBy(category => category.CategoryId).AsQueryable();

        var categoriesOrderedResult = PagedList<Category>.ToPagedList(categoriesOrdered,
                                                                      categoriesParams.PageNumber,
                                                                      categoriesParams.PageSize);
        
        return categoriesOrderedResult;
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

    public async Task<PagedList<Category>> GetCategoriesFilterNameAsync(CategoriesFilterNameParameters categoriesFilterNameParams)
    {
        var categories = await GetAllAsync();

        if (!string.IsNullOrWhiteSpace(categoriesFilterNameParams.CategoryName)) 
        {
            categories = categories.Where(category => category.Name.ToLower().Contains(categoriesFilterNameParams.CategoryName.ToLower())).OrderBy(category => category.Name);
        }

        return PagedList<Category>.ToPagedList(categories.AsQueryable(),
                                               categoriesFilterNameParams.PageNumber,
                                               categoriesFilterNameParams.PageSize);
    }
}
