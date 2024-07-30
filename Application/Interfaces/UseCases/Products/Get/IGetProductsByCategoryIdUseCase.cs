using Application.Pagination;
using Communication.DTOs;

namespace Application.Interfaces.UseCases.Products.Get;

public interface IGetProductsByCategoryIdUseCase
{
    Task<PagedList<ProductDTO>> ExecuteAsync(int categoryId, ProductsParameters productParams);
}
