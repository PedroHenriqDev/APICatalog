using Catalog.Application.Interfaces;
using Catalog.Application.Interfaces.Providers.Products;
using Catalog.Application.Interfaces.UseCases.Products.Delete;
using Catalog.Application.UseCases.Products.Delete;
using AutoMapper;

namespace Catalog.Application.Providers.Products;

public class DeleteProductUseCaseProvider : BaseProvider, IDeleteProductUseCaseProvider
{
    private IDeleteProductByIdUseCase deleteByIdUseCase;

    public DeleteProductUseCaseProvider(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public IDeleteProductByIdUseCase DeleteByIdUseCase => deleteByIdUseCase 
        ??= new DeleteProductByIdUseCase(_unitOfWork, _mapper);
}

