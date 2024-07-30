using Application.Interfaces;
using Application.Interfaces.Providers.Products;
using Application.Interfaces.UseCases.Products.Patch;
using Application.UseCases.Products.Patch;
using Application.Validations.Products;
using AutoMapper;

namespace Application.Providers.Products;

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
