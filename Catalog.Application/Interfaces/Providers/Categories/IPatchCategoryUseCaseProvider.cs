using Catalog.Application.Interfaces.UseCases.Categories.Patch;

namespace Catalog.Application.Interfaces.Providers.Categories;

public interface IPatchCategoryUseCaseProvider
{
    IPatchCategoryUseCase PatchUseCase { get; }
}
