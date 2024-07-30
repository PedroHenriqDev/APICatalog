using Application.Interfaces.UseCases.Categories.Put;

namespace Application.Interfaces.Providers.Categories;

public interface IPutCategoryUseCaseProvider
{
    IUpdateCategoryUseCase UpdateUseCase { get; }
}
