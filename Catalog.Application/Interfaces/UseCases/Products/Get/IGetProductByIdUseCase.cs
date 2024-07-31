using Catalog.Communication.DTOs;

namespace Catalog.Application.Interfaces.UseCases.Products.Get;

public interface IGetProductByIdUseCase
{
     Task<ProductDTO> ExecuteAsync(int id);
}
