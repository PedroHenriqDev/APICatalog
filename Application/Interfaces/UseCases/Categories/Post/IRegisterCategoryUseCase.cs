using Communication.DTOs;

namespace Application.Interfaces.UseCases.Categories.Post;

public interface IRegisterCategoryUseCase
{
    Task<CategoryDTO> ExecuteAsync(CategoryDTO categoryDTO);
}
