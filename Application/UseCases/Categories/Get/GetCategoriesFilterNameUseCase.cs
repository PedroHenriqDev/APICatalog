using Application.Interfaces;
using Application.Interfaces.UseCases.Categories.Get;
using Application.Pagination;
using Application.Validations;
using AutoMapper;
using Communication.DTOs;
using Configuration.Resources;
using ExceptionManager.ExceptionBase;
using Infrastructure.Domain;

namespace Application.UseCases.Categories.Get;

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
