using Application.UseCases.Categories.Get;

namespace Application.Interfaces.Providers.Categories;

public interface IGetCategoryUseCaseProvider
{
    GetCategoryByIdUseCase GetByIdUseCase { get; }

    GetCategoryByIdWithProductsUseCase GetByIdWithProductsUseCase { get; }

    GetCategoriesFilterNameUseCase GetFilterNameUseCase { get; }

    GetCategoriesUseCase GetUseCase { get; }
}
