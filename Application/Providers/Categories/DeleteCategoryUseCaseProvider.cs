using Application.Interfaces;
using Application.Interfaces.Providers.Categories;
using Application.Interfaces.UseCases.Categories.Delete;
using Application.UseCases.Categories.Delete;
using AutoMapper;

namespace Application.Providers.Categories;

public class DeleteCategoryUseCaseProvider : BaseProvider, IDeleteCategoryUseCaseProvider
{
    private IDeleteCategoryByIdUseCase deleteByIdUseCase;

    public DeleteCategoryUseCaseProvider(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public IDeleteCategoryByIdUseCase DeleteByIdUseCase => deleteByIdUseCase 
        ??= new DeleteCategoryByIdUseCase(_unitOfWork, _mapper);
}
