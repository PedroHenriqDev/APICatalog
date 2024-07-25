using Application.Interfaces;
using Application.Interfaces.Providers.Products;
using Application.UseCases.Products.Patch;
using Application.Validations.Products;
using AutoMapper;

namespace Application.Providers.Products;

public class PatchProductUseCaseProvider : BaseProvider, IPatchProductUseCaseProvider
{
    private readonly RequestProductDTOValidation _validation;

    private PatchProductUseCase patchUseCase;

    public PatchProductUseCaseProvider(IUnitOfWork unitOfWork, IMapper mapper, RequestProductDTOValidation validation) : base(unitOfWork, mapper)
    {
        _validation = validation;
    }

    public PatchProductUseCase PatchUseCase => patchUseCase 
        ??= new PatchProductUseCase(_unitOfWork, _mapper, _validation);
}
