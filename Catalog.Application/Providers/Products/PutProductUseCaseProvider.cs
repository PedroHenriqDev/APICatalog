using Catalog.Application.Interfaces;
using Catalog.Application.Interfaces.Providers.Products;
using Catalog.Application.Interfaces.UseCases.Products.Put;
using Catalog.Application.UseCases.Products.Put;
using Catalog.Application.Validations.Products;
using AutoMapper;

namespace Catalog.Application.Providers.Products;

public class PutProductUseCaseProvider : BaseProvider, IPutProductUseCaseProvider
{
    private IUpdateProductUseCase putUseCase;

    private readonly ProductValidation _validation;

    public PutProductUseCaseProvider(IUnitOfWork unitOfWork, IMapper mapper, ProductValidation validation) : base(unitOfWork, mapper)
    {
        _validation = validation;
    }

    public IUpdateProductUseCase PutUseCase => putUseCase 
        ??= new UpdateProductUseCase(_unitOfWork, _mapper, _validation);
}
