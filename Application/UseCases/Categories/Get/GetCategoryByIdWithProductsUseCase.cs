using Application.Extensions;
using Application.Interfaces;
using Application.Validations;
using AutoMapper;
using Communication.DTOs;
using Configuration.Resources;
using ExceptionManager.ExceptionBase;
using Infrastructure.Domain;

namespace Application.UseCases.Categories.Get;

public class GetCategoryByIdWithProductsUseCase : UseCase
{
    public GetCategoryByIdWithProductsUseCase(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<CategoryDTO> ExecuteAsync(int id) 
    {
        EntityValidatorHelper.ValidId(id);

        var category = await _unitOfWork.CategoryRepository.GetByIdWithProductsAsync(id);

        EntityValidatorHelper.ValidateNotNull<Category, NotFoundException>(category,
            ErrorMessagesResource.PRODUCT_ID_NOT_FOUND.FormatErrorMessage(id));

        return _mapper.Map<CategoryDTO>(category);
    }
}
