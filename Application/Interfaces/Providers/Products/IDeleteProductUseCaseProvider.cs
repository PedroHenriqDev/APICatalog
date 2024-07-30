using Application.Interfaces.UseCases.Products.Delete;

namespace Application.Interfaces.Providers.Products;

public interface IDeleteProductUseCaseProvider
{
    IDeleteProductByIdUseCase DeleteByIdUseCase { get; }
}
