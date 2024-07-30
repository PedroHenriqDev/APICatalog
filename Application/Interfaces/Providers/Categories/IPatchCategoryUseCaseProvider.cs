using Application.Interfaces.UseCases.Categories.Patch;

namespace Application.Interfaces.Providers.Categories;

public interface IPatchCategoryUseCaseProvider
{
    IPatchCategoryUseCase PatchUseCase { get; }
}
