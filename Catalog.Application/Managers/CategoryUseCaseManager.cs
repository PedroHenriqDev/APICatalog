using Catalog.Application.Interfaces.Managers;
using Catalog.Application.Interfaces.Providers.Categories;

namespace Catalog.Application.Managers;

public class CategoryUseCaseManager : ICategoryUseCaseManager
{
    public IGetCategoryUseCaseProvider GetProvider { get; }
    public IPostCategoryUseCaseProvider PostProvider { get; }
    public IPatchCategoryUseCaseProvider PatchProvider { get; }
    public IPutCategoryUseCaseProvider PutProvider { get; }
    public IDeleteCategoryUseCaseProvider DeleteProvider { get; }

    public CategoryUseCaseManager(IGetCategoryUseCaseProvider getProvider,
                                  IPostCategoryUseCaseProvider postProvider,
                                  IPatchCategoryUseCaseProvider patchProvider, 
                                  IPutCategoryUseCaseProvider putProvider, 
                                  IDeleteCategoryUseCaseProvider deleteProvider)
    {
        GetProvider = getProvider;
        PostProvider = postProvider;
        PatchProvider = patchProvider;
        PutProvider = putProvider;
        DeleteProvider = deleteProvider;
    }
}
