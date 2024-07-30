using Communication.DTOs;

namespace Application.Interfaces.UseCases.Products.Put;

public interface IUpdateProductUseCase
{
    Task<ProductDTO> ExecuteAsync(int id, ProductDTO product);
}
