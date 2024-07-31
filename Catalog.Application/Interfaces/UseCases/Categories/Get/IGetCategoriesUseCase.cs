using Catalog.Application.Pagination;
using Catalog.Communication.DTOs;

namespace Catalog.Application.Interfaces.UseCases.Categories.Get;

public interface IGetCategoriesUseCase
{
    Task<PagedList<CategoryDTO>> ExecuteAsync(CategoriesParameters categoriesParams);
}
