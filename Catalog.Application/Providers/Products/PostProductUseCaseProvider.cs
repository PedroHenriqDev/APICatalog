using Catalog.Application.Interfaces;
using Catalog.Application.Interfaces.Providers.Products;
using Catalog.Application.Interfaces.UseCases.Products.Post;
using Catalog.Application.UseCases.Products.Post;
using Catalog.Application.Validations.Products;
using AutoMapper;

namespace Catalog.Application.Providers.Products;

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
