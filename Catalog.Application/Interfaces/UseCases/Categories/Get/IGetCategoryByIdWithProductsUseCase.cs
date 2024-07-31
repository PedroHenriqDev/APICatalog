using Catalog.Communication.DTOs;

namespace Catalog.Application.Interfaces.UseCases.Categories.Get;

public interface IGetCategoryByIdWithProductsUseCase
{
    Task<CategoryDTO> ExecuteAsync(int id);
}
