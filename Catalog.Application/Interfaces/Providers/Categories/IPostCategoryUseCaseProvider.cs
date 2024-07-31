using Catalog.Application.Interfaces.UseCases.Categories.Post;

namespace Catalog.Application.Interfaces.Providers.Categories;

public interface IPostCategoryUseCaseProvider
{
    IRegisterCategoryUseCase RegisterUseCase { get; }
}
