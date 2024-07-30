using Communication.DTOs;

namespace Application.Interfaces.UseCases.Categories.Delete;

public interface IDeleteCategoryByIdUseCase
{
    Task<CategoryDTO> ExecuteAsync(int id);
}
