using Communication.DTOs;

namespace Application.Interfaces.UseCases.Categories.Get;

public interface IGetCategoryByIdUseCase
{
    Task<CategoryDTO> ExecuteAsync(int id);
}
