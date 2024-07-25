using Application.Interfaces;
using Application.Pagination;
using Application.Validations;
using AutoMapper;
using Communication.DTOs;
using Configuration.Resources;
using ExceptionManager.ExceptionBase;
using Infrastructure.Domain;

namespace Application.UseCases.Categories.Get;

public class GetCategoriesUseCase : UseCase
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
