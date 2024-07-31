using AutoMapper;
using Catalog.Application.Interfaces;
using Catalog.Application.Interfaces.UseCases.Categories.Get;
using Catalog.Application.Pagination;
using Catalog.Application.Validations;
using Catalog.Communication.DTOs;
using Catalog.Configuration.Resources;
using Catalog.Domain;
using Catalog.ExceptionManager.ExceptionBase;

namespace Catalog.Application.UseCases.Categories.Get;

public class GetCategoriesFilterNameUseCase : UseCase, IGetCategoriesFilterNameUseCase
{
    public GetCategoriesFilterNameUseCase(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<PagedList<CategoryDTO>> ExecuteAsync(CategoriesFilterNameParameters categoriesParams) 
    {
        var categories = await _unitOfWork.CategoryRepository.GetCategoriesFilterNameAsync(categoriesParams);

        EntityValidatorHelper.ValidateEnumerableNotEmpty<Category, NotFoundException>(categories, ErrorMessagesResource.CATEGORIES_NAME_NOT_FOUND);
    
        return _mapper.Map<PagedList<CategoryDTO>>(categories);
    }
}
