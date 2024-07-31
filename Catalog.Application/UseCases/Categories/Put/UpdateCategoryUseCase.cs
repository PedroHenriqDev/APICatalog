using Catalog.Application.Extensions;
using Catalog.Application.Interfaces;
using Catalog.Application.Interfaces.UseCases.Categories.Put;
using Catalog.Application.Validations;
using Catalog.Application.Validations.Categories;
using AutoMapper;
using Catalog.Communication.DTOs;
using Catalog.Configuration.Resources;
using Catalog.ExceptionManager.ExceptionBase;
using FluentValidation;
using Catalog.Domain;

namespace Catalog.Application.UseCases.Categories.Put;

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
