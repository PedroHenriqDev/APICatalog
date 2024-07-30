using Application.Interfaces.UseCases.Categories.Post;
using Application.UseCases.Categories.Post;

namespace Application.Interfaces.Providers.Categories;

public interface IPostCategoryUseCaseProvider
{
    IRegisterCategoryUseCase RegisterUseCase { get; }
}
