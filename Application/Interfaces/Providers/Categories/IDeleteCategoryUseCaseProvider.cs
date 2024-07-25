using Application.UseCases.Categories.Delete;

namespace Application.Interfaces.Providers.Categories;

public interface IDeleteCategoryUseCaseProvider
{
    DeleteCategoryByIdUseCase DeleteByIdUseCase { get; }
}
