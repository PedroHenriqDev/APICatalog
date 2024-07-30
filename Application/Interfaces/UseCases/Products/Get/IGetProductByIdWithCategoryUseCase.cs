using Communication.DTOs;

namespace Application.Interfaces.UseCases.Products.Get;

public interface IGetProductByIdWithCategoryUseCase
{
    Task<ProductDTO> ExecuteAsync(int id);
}
