using Application.Interfaces;
using Application.Interfaces.Providers.Categories;
using Application.UseCases.Categories.Get;
using AutoMapper;

namespace Application.Providers.Categories;

public class GetCategoryUseCaseProvider : BaseProvider, IGetCategoryUseCaseProvider
{
    private GetCategoryByIdUseCase getByIdUseCase;
    private GetCategoryByIdWithProductsUseCase getByIdWithProductsUseCase;
    private GetCategoriesFilterNameUseCase getFilterNameUseCase;
    private GetCategoriesUseCase getUseCase;

    public GetCategoryUseCaseProvider(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public GetCategoryByIdUseCase GetByIdUseCase => getByIdUseCase
        ??= new GetCategoryByIdUseCase(_unitOfWork, _mapper);

    public GetCategoryByIdWithProductsUseCase GetByIdWithProductsUseCase => getByIdWithProductsUseCase
        ??= new GetCategoryByIdWithProductsUseCase(_unitOfWork, _mapper);

    public GetCategoriesFilterNameUseCase GetFilterNameUseCase => getFilterNameUseCase
        ??= new GetCategoriesFilterNameUseCase(_unitOfWork, _mapper);

    public GetCategoriesUseCase GetUseCase => getUseCase
        ??= new GetCategoriesUseCase(_unitOfWork, _mapper);
}
