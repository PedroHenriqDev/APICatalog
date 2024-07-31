using Catalog.Application.Interfaces;
using Catalog.Application.Interfaces.Providers.Categories;
using Catalog.Application.Interfaces.UseCases.Categories.Patch;
using Catalog.Application.UseCases.Categories.Patch;
using Catalog.Application.Validations.Categories;
using AutoMapper;

namespace Catalog.Application.Providers.Categories;

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

