using Communication.DTOs;

namespace Application.Interfaces.UseCases.Products.Get;

public interface IGetProductByIdUseCase
{
     Task<ProductDTO> ExecuteAsync(int id);
}
