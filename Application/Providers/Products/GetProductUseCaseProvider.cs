using Application.Interfaces;
using Application.Interfaces.Providers.Products;
using Application.Interfaces.UseCases.Products.Get;
using Application.UseCases.Products.Get;
using AutoMapper;

namespace Application.Providers.Products;

public class GetProductUseCaseProvider : BaseProvider, IGetProductUseCaseProvider
{
    private IGetProductsUseCase getUseCase;
    private IGetProductsFilterPriceUseCase getFilterPriceUseCase;
    private IGetProductByIdUseCase getByIdUseCase;
    private IGetProductByIdWithCategoryUseCase getByIdWithCategoryUseCase;
    private IGetProductsByCategoryIdUseCase getByCategoryIdUseCase;

    public GetProductUseCaseProvider(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public IGetProductsUseCase GetUseCase => getUseCase 
        ??= new GetProductsUseCase(_unitOfWork, _mapper);

    public IGetProductsFilterPriceUseCase GetFilterPriceUseCase => getFilterPriceUseCase 
        ??= new GetProductsFilterPriceUseCase(_unitOfWork, _mapper);

    public IGetProductByIdUseCase GetByIdUseCase => getByIdUseCase 
        ??= new GetProductByIdUseCase(_unitOfWork, _mapper);

    public IGetProductByIdWithCategoryUseCase GetByIdWithCategoryUseCase => getByIdWithCategoryUseCase 
        ??= new GetProductByIdWithCategoryUseCase(_unitOfWork, _mapper);

    public IGetProductsByCategoryIdUseCase GetByCategoryIdUseCase => getByCategoryIdUseCase 
        ??= new GetProductsByCategoryIdUseCase(_unitOfWork, _mapper);
}
