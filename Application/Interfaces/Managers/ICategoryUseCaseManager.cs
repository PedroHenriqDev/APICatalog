using Application.Interfaces.Providers.Categories;

namespace Application.Interfaces.Managers;

public interface ICategoryUseCaseManager
{
    IGetCategoryUseCaseProvider GetProvider { get; }
    IPostCategoryUseCaseProvider PostProvider { get; }
    IPatchCategoryUseCaseProvider PatchProvider { get; }
    IPutCategoryUseCaseProvider PutProvider { get; }
    IDeleteCategoryUseCaseProvider DeleteProvider { get; }
}
