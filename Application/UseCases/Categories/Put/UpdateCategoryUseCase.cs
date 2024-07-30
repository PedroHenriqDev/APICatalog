using Application.Extensions;
using Application.Interfaces;
using Application.Interfaces.UseCases.Categories.Put;
using Application.Validations;
using Application.Validations.Categories;
using AutoMapper;
using Communication.DTOs;
using Configuration.Resources;
using ExceptionManager.ExceptionBase;
using Infrastructure.Domain;

namespace Application.UseCases.Categories.Put;

public class UpdateCategoryUseCase : UseCase, IUpdateCategoryUseCase
{
    private readonly CategoryValidation _validation;

    public UpdateCategoryUseCase(IUnitOfWork unitOfWork, IMapper mapper, CategoryValidation validation) : base(unitOfWork, mapper)
    {
        _validation = validation;
    }

    public async Task<CategoryDTO> ExecuteAsync(int id, CategoryDTO categoryDTO) 
    {
        Validate(categoryDTO);

        var categoryExisting = await _unitOfWork.CategoryRepository.GetAsync(category => category.CategoryId == id);

        EntityValidatorHelper.ValidateNotNull<Category, NotFoundException>(categoryExisting, ErrorMessagesResource.CATEGORY_ID_NOT_FOUND.FormatErrorMessage(categoryDTO.CategoryId));

        var categoryToUpdate = _mapper.Map(categoryDTO, categoryExisting);

        var category = _unitOfWork.CategoryRepository.Update(categoryToUpdate!);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<CategoryDTO>(category);
    }

    private void Validate(CategoryDTO categoryDTO) 
    {
        var result = _validation.Validate(categoryDTO);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
