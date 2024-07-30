using Application.Pagination;
using Communication.DTOs;
using Infrastructure.Domain;

namespace Application.Interfaces.UseCases.Categories.Get;

public interface IGetCategoriesFilterNameUseCase
{
    Task<PagedList<CategoryDTO>> ExecuteAsync(CategoriesFilterNameParameters categoriesParams);
}
