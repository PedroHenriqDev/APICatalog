using Catalog.Communication.DTOs;

namespace Catalog.Application.Interfaces.UseCases.Products.Delete;

public interface IDeleteProductByIdUseCase
{
    Task<ProductDTO> ExecuteAsync(int id);
}
