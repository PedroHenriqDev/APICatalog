using Catalog.Communication.DTOs;

namespace Catalog.Application.Interfaces.UseCases.Categories.Post;

public interface IRegisterCategoryUseCase
{
    Task<CategoryDTO> ExecuteAsync(CategoryDTO categoryDTO);
}
