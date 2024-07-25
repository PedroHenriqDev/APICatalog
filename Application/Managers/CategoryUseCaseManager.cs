using Application.Interfaces.Managers;
using Application.Interfaces.Providers.Categories;

namespace Application.Managers;

public class CategoryUseCaseManager : ICategoryUseCaseManager
{
    public IGetCategoryUseCaseProvider GetProvider { get; }
    public IPostCategoryUseCaseProvider PostProvider { get; }
    public IPatchCategoryUseCaseProvider PatchProvider { get; }
    public IPutCategoryUseCaseProvider PutProvider { get; }
    public IDeleteCategoryUseCaseProvider DeleteProvider { get; }
    public IStatsCategoryUseCaseProvider StatsProvider { get; }

    public CategoryUseCaseManager(IGetCategoryUseCaseProvider getProvider,
                                  IPostCategoryUseCaseProvider postProvider,
                                  IPatchCategoryUseCaseProvider patchProvider, 
                                  IPutCategoryUseCaseProvider putProvider, 
                                  IDeleteCategoryUseCaseProvider deleteProvider,
                                  IStatsCategoryUseCaseProvider statsProvider)
    {
        GetProvider = getProvider;
        PostProvider = postProvider;
        PatchProvider = patchProvider;
        PutProvider = putProvider;
        DeleteProvider = deleteProvider;
        StatsProvider = statsProvider;
    }
}
