using Catalog.Communication.DTOs;

namespace Catalog.Application.Interfaces.UseCases.Products.Post;

public interface IRegisterProductUseCase
{
    Task<ProductDTO> ExecuteAsync(ProductDTO productDTO);
}
