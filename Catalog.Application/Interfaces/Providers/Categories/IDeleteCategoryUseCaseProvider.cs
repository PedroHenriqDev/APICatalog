using Catalog.Application.Interfaces.UseCases.Categories.Delete;

namespace Catalog.Application.Interfaces.Providers.Categories;

public interface IDeleteCategoryUseCaseProvider
{
    IDeleteCategoryByIdUseCase DeleteByIdUseCase { get; }
}
