using Catalog.Application.Pagination;
using Catalog.Communication.DTOs;

namespace Catalog.Application.Interfaces.UseCases.Products.Get;

public interface IGetProductsFilterPriceUseCase
{
    Task<PagedList<ProductDTO>> ExecuteAsync(ProductsFilterPriceParameters productParams);
}
