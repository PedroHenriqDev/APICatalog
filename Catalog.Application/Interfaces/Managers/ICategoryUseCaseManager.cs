using Catalog.Application.Interfaces.Providers.Categories;

namespace Catalog.Application.Interfaces.Managers;

public interface ICategoryUseCaseManager
{
    IGetCategoryUseCaseProvider GetProvider { get; }
    IPostCategoryUseCaseProvider PostProvider { get; }
    IPatchCategoryUseCaseProvider PatchProvider { get; }
    IPutCategoryUseCaseProvider PutProvider { get; }
    IDeleteCategoryUseCaseProvider DeleteProvider { get; }
}
