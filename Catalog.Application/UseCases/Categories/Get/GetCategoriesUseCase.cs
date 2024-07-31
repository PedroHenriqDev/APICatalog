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

public class GetCategoriesUseCase : UseCase, IGetCategoriesUseCase
{
    public GetCategoriesUseCase(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) 
    {
    }

    public async Task<PagedList<CategoryDTO>> ExecuteAsync(CategoriesParameters categoriesParams) 
    {
        var categories = await _unitOfWork.CategoryRepository.GetCategoriesAsync(categoriesParams);
        
        EntityValidatorHelper.ValidateEnumerableNotEmpty<Category, NotFoundException>(categories, ErrorMessagesResource.NOT_FOUND);

        return _mapper.Map<PagedList<CategoryDTO>>(categories);
    }
}
