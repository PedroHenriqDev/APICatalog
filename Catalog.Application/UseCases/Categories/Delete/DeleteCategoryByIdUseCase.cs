using Catalog.Application.Extensions;
using Catalog.Application.Interfaces;
using Catalog.Application.Interfaces.UseCases.Categories.Delete;
using Catalog.Application.Validations;
using AutoMapper;
using Catalog.Communication.DTOs;
using Catalog.Configuration.Resources;
using Catalog.ExceptionManager.ExceptionBase;
using Catalog.Domain;

namespace Catalog.Application.UseCases.Categories.Delete;

public class DeleteCategoryByIdUseCase : UseCase, IDeleteCategoryByIdUseCase
{
    public DeleteCategoryByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)  
    {
    }

    public async Task<CategoryDTO> ExecuteAsync(int id) 
    {
        EntityValidatorHelper.ValidId(id);

        var category = await _unitOfWork.CategoryRepository.GetAsync(category => category.CategoryId == id);

        EntityValidatorHelper.ValidateNotNull<Category, NotFoundException>(category, ErrorMessagesResource.CATEGORY_ID_NOT_FOUND.FormatErrorMessage(id));

        _unitOfWork.CategoryRepository.Delete(category!);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<CategoryDTO>(category);
    }
}
