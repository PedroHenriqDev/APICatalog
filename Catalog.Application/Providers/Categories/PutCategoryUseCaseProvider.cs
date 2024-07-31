using Catalog.Application.Interfaces;
using Catalog.Application.Interfaces.Providers.Categories;
using Catalog.Application.Interfaces.UseCases.Categories.Put;
using Catalog.Application.UseCases.Categories.Post;
using Catalog.Application.UseCases.Categories.Put;
using Catalog.Application.Validations.Categories;
using AutoMapper;

namespace Catalog.Application.Providers.Categories;

public class PutCategoryUseCaseProvider : BaseProvider, IPutCategoryUseCaseProvider
{
    private readonly CategoryValidation _validation;

    private IUpdateCategoryUseCase updateCategoryUseCase;

    public PutCategoryUseCaseProvider(IUnitOfWork unitOfWork, IMapper mapper, CategoryValidation validation) : base(unitOfWork, mapper)
    {
        _validation = validation;
    }

    public IUpdateCategoryUseCase UpdateUseCase => updateCategoryUseCase
        ??= new UpdateCategoryUseCase(_unitOfWork, _mapper, _validation);
}
