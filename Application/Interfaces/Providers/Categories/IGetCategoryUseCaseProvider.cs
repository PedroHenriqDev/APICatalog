using Application.Interfaces.UseCases.Categories.Get;

namespace Application.Interfaces.Providers.Categories;

public interface IGetCategoryUseCaseProvider
{
    IGetCategoryByIdUseCase GetByIdUseCase { get; }

    IGetCategoryByIdWithProductsUseCase GetByIdWithProductsUseCase { get; }

    IGetCategoriesFilterNameUseCase GetFilterNameUseCase { get; }

    IGetCategoriesUseCase GetUseCase { get; }

    IGetCategoriesStatsUseCase GetStatsUseCase { get; }
}
