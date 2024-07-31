using Catalog.Communication.DTOs;

namespace Catalog.Application.Interfaces.UseCases.Categories.Put;

public interface IUpdateCategoryUseCase
{
    Task<CategoryDTO> ExecuteAsync(int id, CategoryDTO categoryDTO);
}
