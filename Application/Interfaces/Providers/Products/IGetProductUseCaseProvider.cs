using Application.Interfaces.UseCases.Products.Get;

namespace Application.Interfaces.Providers.Products;

public interface IGetProductUseCaseProvider
{
    IGetProductsUseCase GetUseCase { get; }

    IGetProductsFilterPriceUseCase GetFilterPriceUseCase { get; }

    IGetProductByIdUseCase GetByIdUseCase { get; }

    IGetProductByIdWithCategoryUseCase GetByIdWithCategoryUseCase { get; }

    IGetProductsByCategoryIdUseCase GetByCategoryIdUseCase { get; }
}
