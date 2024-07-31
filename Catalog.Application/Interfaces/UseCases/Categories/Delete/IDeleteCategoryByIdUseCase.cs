using Catalog.Communication.DTOs;

namespace Catalog.Application.Interfaces.UseCases.Categories.Delete;

public interface IDeleteCategoryByIdUseCase
{
    Task<CategoryDTO> ExecuteAsync(int id);
}
