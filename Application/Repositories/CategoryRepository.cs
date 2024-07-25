using Infrastructure.Data;
using Infrastructure.Domain;
using Application.Interfaces;
using Application.Pagination;
using Microsoft.EntityFrameworkCore;
using Configuration.Resources;
using Application.Extensions;
using ExceptionManager.ExceptionBase;
using Application.Validations;

namespace Application.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<PagedList<Category>> GetCategoriesWithProductsAsync(CategoriesParameters categoriesParams) 
    {
        var categories = await _context.Categories.Include(category => category.Products).ToListAsync();

        var categoriesOrdered = categories.OrderBy(category => category.CategoryId).AsQueryable();


        return PagedList<Category>.ToPagedList(categoriesOrdered,
                                               categoriesParams.PageNumber,
                                               categoriesParams.PageSize);
    }

    public async Task<PagedList<Category>> GetCategoriesAsync(CategoriesParameters categoriesParams)
    {
        var categories = await GetAllAsync();

        var categoriesOrdered = categories.OrderBy(category => category.CategoryId).AsQueryable();

        return PagedList<Category>.ToPagedList(categoriesOrdered,
                                               categoriesParams.PageNumber,
                                               categoriesParams.PageSize);
    }

    public async Task<Category> GetByIdWithProductsAsync(int id)
    {
        var category = await _context.Categories.AsNoTracking()
                                        .Include(category => category.Products)
                                        .FirstOrDefaultAsync(category => category.CategoryId == id);

        return category!;
    }

    public async Task<Category> GetCategoryByNameAsync(string name)
    {
        var category = await _context.Categories.AsNoTracking()
                                        .FirstOrDefaultAsync(category => category.Name == name);

        return category!;
    }

    public async Task<PagedList<Category>> GetCategoriesFilterNameAsync(CategoriesFilterNameParameters categoriesFilterNameParams)
    {
        var categories = await GetAllAsync();

        categories = categories.Where(category => category.Name.ToLower().Contains(categoriesFilterNameParams.CategoryName.ToLower())).OrderBy(category => category.Name);

        return PagedList<Category>.ToPagedList(categories.AsQueryable(),
                                               categoriesFilterNameParams.PageNumber,
                                               categoriesFilterNameParams.PageSize);
    }
}
