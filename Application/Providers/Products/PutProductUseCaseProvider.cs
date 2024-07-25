using Application.Interfaces;
using Application.Interfaces.Providers.Products;
using Application.UseCases.Products.Put;
using Application.Validations.Products;
using AutoMapper;

namespace Application.Providers.Products;

public class PutProductUseCaseProvider : BaseProvider, IPutProductUseCaseProvider
{
    private readonly ProductValidation _validation;

    private PutProductUseCase putUseCase;

    public PutProductUseCaseProvider(IUnitOfWork unitOfWork, IMapper mapper, ProductValidation validation) : base(unitOfWork, mapper)
    {
        _validation = validation;
    }

    public PutProductUseCase PutUseCase => putUseCase 
        ??= new PutProductUseCase(_unitOfWork, _mapper, _validation);
}
