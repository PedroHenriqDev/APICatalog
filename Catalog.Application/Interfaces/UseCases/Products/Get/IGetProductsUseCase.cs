using Catalog.Application.Pagination;
using Catalog.Communication.DTOs;

namespace Catalog.Application.Interfaces.UseCases.Products.Get;

public interface IGetProductsUseCase
{
    Task<PagedList<ProductDTO>> ExecuteAsync(ProductsParameters productParams);
}
