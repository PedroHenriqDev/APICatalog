using Communication.DTOs;

namespace Application.Interfaces.UseCases.Categories.Put;

public interface IUpdateCategoryUseCase
{
    Task<CategoryDTO> ExecuteAsync(int id, CategoryDTO categoryDTO);
}
