using Application.UseCases.Categories.Patch;
using Application.UseCases.Categories.Put;

namespace Application.Interfaces.Providers.Categories;

public interface IPutCategoryUseCaseProvider
{
    UpdateCategoryUseCase UpdateUseCase { get; }
}
