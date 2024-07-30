using Application.Pagination;
using Communication.DTOs;
using Infrastructure.Domain;

namespace Application.Interfaces.UseCases.Categories.Get;

public interface IGetCategoriesUseCase
{
    Task<PagedList<CategoryDTO>> ExecuteAsync(CategoriesParameters categoriesParams);
}
