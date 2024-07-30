using Application.Interfaces;
using Application.Interfaces.Providers.Categories;
using Application.Interfaces.UseCases.Categories.Patch;
using Application.UseCases.Categories.Patch;
using Application.Validations.Categories;
using AutoMapper;

namespace Application.Providers.Categories;

public class PatchCategoryUseCaseProvider : BaseProvider, IPatchCategoryUseCaseProvider
{
    private IPatchCategoryUseCase patchUseCase;

    private readonly RequestPatchCategoryDTOValidation _validation;

    public PatchCategoryUseCaseProvider(IUnitOfWork unitOfWork, IMapper mapper, RequestPatchCategoryDTOValidation validation) : base(unitOfWork, mapper)
    {
        _validation = validation;
    }

    public IPatchCategoryUseCase PatchUseCase => patchUseCase 
        ??= new PatchCategoryUseCase(_unitOfWork, _mapper, _validation);
}

