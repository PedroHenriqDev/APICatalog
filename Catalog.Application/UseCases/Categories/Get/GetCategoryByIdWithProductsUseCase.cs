using Catalog.Application.Extensions;
using Catalog.Application.Interfaces;
using Catalog.Application.Interfaces.UseCases.Categories.Get;
using Catalog.Application.Validations;
using AutoMapper;
using Catalog.Communication.DTOs;
using Catalog.Configuration.Resources;
using Catalog.ExceptionManager.ExceptionBase;
using Catalog.Domain;

namespace Catalog.Application.UseCases.Categories.Get;

public class GetCategoryByIdWithProductsUseCase : UseCase, IGetCategoryByIdWithProductsUseCase
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
