using Catalog.Application.Interfaces.UseCases.Products.Get;

namespace Catalog.Application.Interfaces.Providers.Products;

public interface IGetProductUseCaseProvider
{
    IGetProductsUseCase GetUseCase { get; }

    IGetProductsFilterPriceUseCase GetFilterPriceUseCase { get; }

    IGetProductByIdUseCase GetByIdUseCase { get; }

    IGetProductByIdWithCategoryUseCase GetByIdWithCategoryUseCase { get; }

    IGetProductsByCategoryIdUseCase GetByCategoryIdUseCase { get; }
}
