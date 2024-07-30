using Application.Interfaces;
using Application.Interfaces.Providers.Categories;
using Application.Interfaces.UseCases.Categories.Post;
using Application.UseCases.Categories.Post;
using Application.Validations.Categories;
using AutoMapper;

namespace Application.Providers.Categories;

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
