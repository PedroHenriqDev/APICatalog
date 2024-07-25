using Application.Interfaces;
using Application.Interfaces.Providers.Products;
using Application.UseCases.Products.Post;
using Application.Validations.Products;
using AutoMapper;

namespace Application.Providers.Products;

public class PostProductUseCaseProvider : BaseProvider, IPostProductUseCaseProvider
{
    private readonly ProductValidation _validation;

    private RegisterProductUseCase registerUseCase;

    public PostProductUseCaseProvider(IUnitOfWork unitOfWork, IMapper mapper, ProductValidation validation) : base(unitOfWork, mapper)
    {
        _validation = validation;
    }

    public RegisterProductUseCase RegisterUseCase => registerUseCase 
        ??= new RegisterProductUseCase(_unitOfWork, _mapper, _validation);
}
