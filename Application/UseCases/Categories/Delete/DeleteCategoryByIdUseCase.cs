using Application.Extensions;
using Application.Interfaces;
using Application.Validations;
using AutoMapper;
using Communication.DTOs;
using Configuration.Resources;
using ExceptionManager.ExceptionBase;
using Infrastructure.Domain;

namespace Application.UseCases.Categories.Delete;

public class DeleteCategoryByIdUseCase : UseCase
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
