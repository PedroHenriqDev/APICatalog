using Application.UseCases.Categories.Post;

namespace Application.Interfaces.Providers.Categories;

public interface IPostCategoryUseCaseProvider
{
    RegisterCategoryUseCase RegisterUseCase { get; }
}
