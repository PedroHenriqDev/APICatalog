using Catalog.Application.Interfaces.UseCases.Categories.Put;

namespace Catalog.Application.Interfaces.Providers.Categories;

public interface IPutCategoryUseCaseProvider
{
    IUpdateCategoryUseCase UpdateUseCase { get; }
}
