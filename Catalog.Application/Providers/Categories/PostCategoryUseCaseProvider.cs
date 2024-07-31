using Catalog.Application.Interfaces;
using Catalog.Application.Interfaces.Providers.Categories;
using Catalog.Application.Interfaces.UseCases.Categories.Post;
using Catalog.Application.UseCases.Categories.Post;
using Catalog.Application.Validations.Categories;
using AutoMapper;

namespace Catalog.Application.Providers.Categories;

public class PostCategoryUseCaseProvider : BaseProvider, IPostCategoryUseCaseProvider
{
    private readonly CategoryValidation _validation;

    private IRegisterCategoryUseCase registerUseCase;

    public PostCategoryUseCaseProvider(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public IRegisterCategoryUseCase RegisterUseCase => registerUseCase
        ??= new RegisterCategoryUseCase(_unitOfWork, _mapper, _validation);
}
