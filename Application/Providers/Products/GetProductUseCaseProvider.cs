using Application.Interfaces;
using Application.Interfaces.Providers.Products;
using Application.UseCases.Products.Get;
using AutoMapper;

namespace Application.Providers.Products;

public class GetProductUseCaseProvider : BaseProvider, IGetProductUseCaseProvider
{
    private GetProductsUseCase getUseCase;
    private GetProductsFilterPriceUseCase getFilterPriceUseCase;
    private GetProductByIdUseCase getByIdUseCase;
    private GetProductByIdWithCategoryUseCase getByIdWithCategoryUseCase;
    private GetProductsByCategoryIdUseCase getByCategoryIdUseCase;

    public GetProductUseCaseProvider(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public GetProductsUseCase GetUseCase => getUseCase 
        ??= new GetProductsUseCase(_unitOfWork, _mapper);

    public GetProductsFilterPriceUseCase GetFilterPriceUseCase => getFilterPriceUseCase 
        ??= new GetProductsFilterPriceUseCase(_unitOfWork, _mapper);

    public GetProductByIdUseCase GetByIdUseCase => getByIdUseCase 
        ??= new GetProductByIdUseCase(_unitOfWork, _mapper);

    public GetProductByIdWithCategoryUseCase GetByIdWithCategoryUseCase => getByIdWithCategoryUseCase 
        ??= new GetProductByIdWithCategoryUseCase(_unitOfWork, _mapper);

    public GetProductsByCategoryIdUseCase GetByCategoryIdUseCase => getByCategoryIdUseCase 
        ??= new GetProductsByCategoryIdUseCase(_unitOfWork, _mapper);
}
