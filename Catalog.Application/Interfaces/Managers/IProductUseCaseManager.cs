using Catalog.Application.Interfaces.Providers.Products;
using Catalog.Application.Providers.Products;

namespace Catalog.Application.Interfaces.Managers;

public interface IProductUseCaseManager
{
    IGetProductUseCaseProvider GetProvider { get; }
    IPostProductUseCaseProvider PostProvider { get; }
    IPatchProductUseCaseProvider PatchProvider { get; }
    IPutProductUseCaseProvider PutProvider { get; }
    IDeleteProductUseCaseProvider DeleteProvider { get; }
}
