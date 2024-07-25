using Application.Interfaces;
using Application.Interfaces.Providers.Categories;
using Application.UseCases.Categories.Patch;
using Application.Validations.Categories;
using AutoMapper;

namespace Application.Providers.Categories;

public class PatchCategoryUseCaseProvider : BaseProvider, IPatchCategoryUseCaseProvider
{
    private readonly RequestPatchCategoryDTOValidation _validation;

    private PatchCategoryUseCase patchUseCase;

    public PatchCategoryUseCaseProvider(IUnitOfWork unitOfWork, IMapper mapper, RequestPatchCategoryDTOValidation validation) : base(unitOfWork, mapper)
    {
        _validation = validation;
    }

    public PatchCategoryUseCase PatchUseCase => patchUseCase 
        ??= new PatchCategoryUseCase(_unitOfWork, _mapper, _validation);
}

