using Application.Interfaces.UseCases.Products.Post;

namespace Application.Interfaces.Providers.Products;

public interface IPostProductUseCaseProvider
{
    IRegisterProductUseCase RegisterUseCase { get; }
}
