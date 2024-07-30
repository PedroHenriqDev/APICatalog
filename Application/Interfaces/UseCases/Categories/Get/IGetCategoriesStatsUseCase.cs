using Application.Pagination;
using Communication.DTOs.Responses;

namespace Application.Interfaces.UseCases.Categories.Get;

public interface IGetCategoriesStatsUseCase
{
    Task<ResponseCategoriesStatsDTO> ExecuteAsync(CategoriesParameters categoriesParameters);
}
