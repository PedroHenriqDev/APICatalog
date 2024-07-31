using Catalog.Application.Pagination;
using Catalog.Communication.DTOs;

namespace Catalog.Application.Interfaces.UseCases.Products.Get;

public interface IGetProductsByCategoryIdUseCase
{
    Task<PagedList<ProductDTO>> ExecuteAsync(int categoryId, ProductsParameters productParams);
}
