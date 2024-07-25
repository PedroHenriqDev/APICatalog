using Application.UseCases.Categories.Patch;

namespace Application.Interfaces.Providers.Categories;

public interface IPatchCategoryUseCaseProvider
{
    PatchCategoryUseCase PatchUseCase { get; }
}
