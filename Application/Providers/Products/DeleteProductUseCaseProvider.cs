using Application.Interfaces;
using Application.Interfaces.Providers.Products;
using Application.UseCases.Products.Delete;
using AutoMapper;

namespace Application.Providers.Products;

public class DeleteProductUseCaseProvider : BaseProvider, IDeleteProductUseCaseProvider
{
    private DeleteProductByIdUseCase deleteByIdUseCase;

    public DeleteProductUseCaseProvider(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public DeleteProductByIdUseCase DeleteByIdUseCase => deleteByIdUseCase 
        ??= new DeleteProductByIdUseCase(_unitOfWork, _mapper);
}

