using Catalog.Application.Interfaces;
using Catalog.Application.Interfaces.Providers.Products;
using Catalog.Application.Interfaces.UseCases.Products.Patch;
using Catalog.Application.UseCases.Products.Patch;
using Catalog.Application.Validations.Products;
using AutoMapper;

namespace Catalog.Application.Providers.Products;

public class PatchProductUseCaseProvider : BaseProvider, IPatchProductUseCaseProvider
{
    private readonly RequestProductDTOValidation _validation;

    private IPatchProductUseCase patchUseCase;

    public PatchProductUseCaseProvider(IUnitOfWork unitOfWork, IMapper mapper, RequestProductDTOValidation validation) : base(unitOfWork, mapper)
    {
        _validation = validation;
    }

    public IPatchProductUseCase PatchUseCase => patchUseCase 
        ??= new PatchProductUseCase(_unitOfWork, _mapper, _validation);
}
