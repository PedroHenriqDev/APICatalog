using Application.Interfaces.UseCases.Categories.Delete;
using Application.UseCases.Categories.Delete;

namespace Application.Interfaces.Providers.Categories;

public interface IDeleteCategoryUseCaseProvider
{
    IDeleteCategoryByIdUseCase DeleteByIdUseCase { get; }
}
