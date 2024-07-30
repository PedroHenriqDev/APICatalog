using Communication.DTOs;

namespace Application.Interfaces.UseCases.Products.Delete;

public interface IDeleteProductByIdUseCase
{
    Task<ProductDTO> ExecuteAsync(int id);
}
