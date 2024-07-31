using Catalog.Application.Interfaces;
using Catalog.Application.Interfaces.Providers.Categories;
using Catalog.Application.Interfaces.UseCases.Categories.Delete;
using Catalog.Application.UseCases.Categories.Delete;
using AutoMapper;

namespace Catalog.Application.Providers.Categories;

public class DeleteCategoryUseCaseProvider : BaseProvider, IDeleteCategoryUseCaseProvider
{
    private IDeleteCategoryByIdUseCase deleteByIdUseCase;

    public DeleteCategoryUseCaseProvider(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public IDeleteCategoryByIdUseCase DeleteByIdUseCase => deleteByIdUseCase 
        ??= new DeleteCategoryByIdUseCase(_unitOfWork, _mapper);
}
