using Application.Pagination;
using Communication.DTOs;

namespace Application.Interfaces.UseCases.Products.Get;

public interface IGetProductsUseCase
{
    Task<PagedList<ProductDTO>> ExecuteAsync(ProductsParameters productParams);
}
