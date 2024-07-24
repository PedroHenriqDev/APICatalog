using Infrastructure.Data;
using Infrastructure.Domain;
using Application.Interfaces;
using Application.Pagination;
using Microsoft.EntityFrameworkCore;
using Application.Validators;
using Configuration.Resources;
using Application.Extensions;
using ExceptionManager.ExceptionBase;

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
        EntityValidator.ValidId<InvalidValueException>(id, ErrorMessagesResource.INVALID_ID.FormatErrorMessage(id));

        var category = await _context.Categories.AsNoTracking()
                                        .Include(category => category.Products)
                                        .FirstOrDefaultAsync(category => category.CategoryId == id);

        EntityValidator.ValidateNotNull<Category, NotFoundException>(category, ErrorMessagesResource.CATEGORY_ID_NOT_FOUND.FormatErrorMessage(id));

        return category!;
    }

    public async Task<Category> GetCategoryByNameAsync(string name)
    {
        EntityValidator.ValidateText<InvalidValueException>(name, ErrorMessagesResource.INVALID_CATEGORY_NAME);

        var category = await _context.Categories.AsNoTracking()
                                        .FirstOrDefaultAsync(category => category.Name == name);

        EntityValidator.ValidateNotNull<Category, NotFoundException>(category, ErrorMessagesResource.CATEGORY_NAME_NOT_FOUND.FormatErrorMessage(name));

        return category!;
    }

    public async Task<PagedList<Category>> GetCategoriesFilterNameAsync(CategoriesFilterNameParameters categoriesFilterNameParams)
    {
        EntityValidator.ValidateText<InvalidValueException>(categoriesFilterNameParams.CategoryName, ErrorMessagesResource.INVALID_CATEGORY_NAME);

        var categories = await GetAllAsync();

        EntityValidator.ValidateEnumerableNotEmpty<Category, NotFoundException>(categories, ErrorMessagesResource.CATEGORIES_NAME_NOT_FOUND.FormatErrorMessage(categoriesFilterNameParams.CategoryName));

        categories = categories.Where(category => category.Name.ToLower().Contains(categoriesFilterNameParams.CategoryName.ToLower())).OrderBy(category => category.Name);

        return PagedList<Category>.ToPagedList(categories.AsQueryable(),
                                               categoriesFilterNameParams.PageNumber,
                                               categoriesFilterNameParams.PageSize);
    }
}
