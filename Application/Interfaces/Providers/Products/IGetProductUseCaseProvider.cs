using Application.UseCases.Products.Get;

namespace Application.Interfaces.Providers.Products;

public interface IGetProductUseCaseProvider
{
    GetProductsUseCase GetUseCase { get; }

    GetProductsFilterPriceUseCase GetFilterPriceUseCase { get; }

    GetProductByIdUseCase GetByIdUseCase { get; }

    GetProductByIdWithCategoryUseCase GetByIdWithCategoryUseCase { get; }

    GetProductsByCategoryIdUseCase GetByCategoryIdUseCase { get; }
}
