using Communication.DTOs;

namespace Application.Interfaces.UseCases.Categories.Get;

public interface IGetCategoryByIdWithProductsUseCase
{
    Task<CategoryDTO> ExecuteAsync(int id);
}
