using Catalog.Application.Pagination;
using Catalog.Communication.DTOs;

namespace Catalog.Application.Interfaces.UseCases.Categories.Get;

public interface IGetCategoriesFilterNameUseCase
{
    Task<PagedList<CategoryDTO>> ExecuteAsync(CategoriesFilterNameParameters categoriesParams);
}
