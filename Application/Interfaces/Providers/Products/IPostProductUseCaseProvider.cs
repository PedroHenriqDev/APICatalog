using Application.UseCases.Products.Post;

namespace Application.Interfaces.Providers.Products;

public interface IPostProductUseCaseProvider
{
    RegisterProductUseCase RegisterUseCase { get; }
}
