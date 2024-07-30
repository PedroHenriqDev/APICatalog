using Application.Interfaces;
using Application.Interfaces.Providers.Categories;
using Application.Interfaces.UseCases.Categories.Get;
using Application.UseCases.Categories.Get;
using AutoMapper;

namespace Application.Providers.Categories;

public class GetCategoryUseCaseProvider : BaseProvider, IGetCategoryUseCaseProvider
{
    private IGetCategoryByIdUseCase getByIdUseCase;
    private IGetCategoryByIdWithProductsUseCase getByIdWithProductsUseCase;
    private IGetCategoriesFilterNameUseCase getFilterNameUseCase;
    private IGetCategoriesUseCase getUseCase;
    private IGetCategoriesStatsUseCase getStatsUseCase;

    public GetCategoryUseCaseProvider(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public IGetCategoryByIdUseCase GetByIdUseCase => getByIdUseCase
        ??= new GetCategoryByIdUseCase(_unitOfWork, _mapper);

    public IGetCategoryByIdWithProductsUseCase GetByIdWithProductsUseCase => getByIdWithProductsUseCase
        ??= new GetCategoryByIdWithProductsUseCase(_unitOfWork, _mapper);

    public IGetCategoriesFilterNameUseCase GetFilterNameUseCase => getFilterNameUseCase
        ??= new GetCategoriesFilterNameUseCase(_unitOfWork, _mapper);

    public IGetCategoriesUseCase GetUseCase => getUseCase
        ??= new GetCategoriesUseCase(_unitOfWork, _mapper);

    public IGetCategoriesStatsUseCase GetStatsUseCase => getStatsUseCase 
        ??= new GetCategoriesStatsUseCase(_unitOfWork, _mapper);
}
