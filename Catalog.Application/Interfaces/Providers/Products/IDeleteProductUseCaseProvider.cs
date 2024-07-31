using Catalog.Application.Interfaces.UseCases.Products.Delete;

namespace Catalog.Application.Interfaces.Providers.Products;

public interface IDeleteProductUseCaseProvider
{
    IDeleteProductByIdUseCase DeleteByIdUseCase { get; }
}
