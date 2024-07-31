using Catalog.Communication.DTOs;

namespace Catalog.Application.Interfaces.UseCases.Products.Get;

public interface IGetProductByIdWithCategoryUseCase
{
    Task<ProductDTO> ExecuteAsync(int id);
}
