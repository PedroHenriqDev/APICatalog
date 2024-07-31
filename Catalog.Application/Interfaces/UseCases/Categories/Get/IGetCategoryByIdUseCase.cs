using Catalog.Communication.DTOs;

namespace  Catalog.Application.Interfaces.UseCases.Categories.Get;

public interface IGetCategoryByIdUseCase
{
    Task<CategoryDTO> ExecuteAsync(int id);
}
