using Application.UseCases.Categories.Delete;
using Application.UseCases.Products.Delete;

namespace Application.Interfaces.Providers.Products;

public interface IDeleteProductUseCaseProvider
{
    DeleteProductByIdUseCase DeleteByIdUseCase { get; }
}
