using Communication.DTOs;

namespace Application.Interfaces.UseCases.Products.Post;

public interface IRegisterProductUseCase
{
    Task<ProductDTO> ExecuteAsync(ProductDTO productDTO);
}
