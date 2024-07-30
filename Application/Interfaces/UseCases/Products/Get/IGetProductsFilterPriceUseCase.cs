using Application.Pagination;
using Communication.DTOs;

namespace Application.Interfaces.UseCases.Products.Get;

public interface IGetProductsFilterPriceUseCase
{
    Task<PagedList<ProductDTO>> ExecuteAsync(ProductsFilterPriceParameters productParams);
}
