using Application.Interfaces;
using Application.Interfaces.Providers.Categories;
using Application.UseCases.Categories.Delete;
using AutoMapper;

namespace Application.Providers.Categories;

public class DeleteCategoryUseCaseProvider : BaseProvider, IDeleteCategoryUseCaseProvider
{
    private DeleteCategoryByIdUseCase deleteByIdUseCase;

    public DeleteCategoryUseCaseProvider(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public DeleteCategoryByIdUseCase DeleteByIdUseCase => deleteByIdUseCase 
        ??= new DeleteCategoryByIdUseCase(_unitOfWork, _mapper);
}
