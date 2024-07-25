using Application.Interfaces.Providers.Products;
using Application.Providers.Products;

namespace Application.Interfaces.Managers;

public interface IProductUseCaseManager
{
    IGetProductUseCaseProvider GetProvider { get; }
    IPostProductUseCaseProvider PostProvider { get; }
    IPatchProductUseCaseProvider PatchProvider { get; }
    IPutProductUseCaseProvider PutProvider { get; }
    IDeleteProductUseCaseProvider DeleteProvider { get; }
}
