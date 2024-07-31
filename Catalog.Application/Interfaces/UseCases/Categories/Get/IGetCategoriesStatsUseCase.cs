using Catalog.Application.Pagination;
using Catalog.Communication.DTOs.Responses;

namespace Catalog.Application.Interfaces.UseCases.Categories.Get;

public interface IGetCategoriesStatsUseCase
{
    Task<ResponseCategoriesStatsDTO> ExecuteAsync(CategoriesParameters categoriesParameters);
}
