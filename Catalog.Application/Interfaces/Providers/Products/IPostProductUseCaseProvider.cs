using Catalog.Application.Interfaces.UseCases.Products.Post;

namespace Catalog.Application.Interfaces.Providers.Products;

public interface IPostProductUseCaseProvider
{
    IRegisterProductUseCase RegisterUseCase { get; }
}
