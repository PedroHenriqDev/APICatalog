using Application.Interfaces;
using Application.Interfaces.Providers.Products;
using Application.Interfaces.UseCases.Products.Post;
using Application.UseCases.Products.Post;
using Application.Validations.Products;
using AutoMapper;

namespace Application.Providers.Products;

public class PostProductUseCaseProvider : BaseProvider, IPostProductUseCaseProvider
{
    private readonly ProductValidation _validation;

    private IRegisterProductUseCase registerUseCase;

    public PostProductUseCaseProvider(IUnitOfWork unitOfWork, IMapper mapper, ProductValidation validation) : base(unitOfWork, mapper)
    {
        _validation = validation;
    }

    public IRegisterProductUseCase RegisterUseCase => registerUseCase 
        ??= new RegisterProductUseCase(_unitOfWork, _mapper, _validation);
}
