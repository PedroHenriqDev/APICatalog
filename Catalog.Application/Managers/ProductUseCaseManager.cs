using Catalog.Application.Interfaces.Managers;
using Catalog.Application.Interfaces.Providers.Products;

namespace Catalog.Application.Managers;

public class ProductUseCaseManager : IProductUseCaseManager
{
    public IGetProductUseCaseProvider GetProvider { get; }
    public IPostProductUseCaseProvider PostProvider { get; }
    public IPatchProductUseCaseProvider PatchProvider { get; }
    public IPutProductUseCaseProvider PutProvider { get; }
    public IDeleteProductUseCaseProvider DeleteProvider { get; }

    public ProductUseCaseManager(IGetProductUseCaseProvider getProvider, 
                                 IPostProductUseCaseProvider postProvider,
                                 IPatchProductUseCaseProvider patchProvider,
                                 IPutProductUseCaseProvider putProvider,
                                 IDeleteProductUseCaseProvider deleteProvider)
    {
        GetProvider = getProvider;
        PostProvider = postProvider;
        PatchProvider = patchProvider;
        PutProvider = putProvider;
        DeleteProvider = deleteProvider;
    }
}
