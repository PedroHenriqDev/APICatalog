using Catalog.Application.Extensions;
using Catalog.Application.Interfaces;
using Catalog.Application.Interfaces.UseCases.Categories.Patch;
using Catalog.Application.Validations;
using Catalog.Application.Validations.Categories;
using AutoMapper;
using Catalog.Communication.DTOs.Requests;
using Catalog.Configuration.Resources;
using Catalog.ExceptionManager.ExceptionBase;
using Catalog.Domain;
using Microsoft.AspNetCore.JsonPatch;

namespace Catalog.Application.UseCases.Categories.Patch;

public class PatchCategoryUseCase : UseCase, IPatchCategoryUseCase
{
    private readonly RequestPatchCategoryDTOValidation _validation;

    public PatchCategoryUseCase(IUnitOfWork unitOfWork, IMapper mapper, RequestPatchCategoryDTOValidation validation) : base(unitOfWork, mapper)
    {
        _validation = validation;
    }

    public async Task<Category> ExecuteAsync(int id, JsonPatchDocument<RequestPatchCategoryDTO> requestPatch)
    {
        EntityValidatorHelper.ValidId(id);

        var category = await _unitOfWork.CategoryRepository.GetAsync(category => category.CategoryId == id);

        EntityValidatorHelper.ValidateNotNull<Category, NotFoundException>(category, ErrorMessagesResource.CATEGORY_ID_NOT_FOUND.FormatErrorMessage(id));

        var requestCategoryDTO = _mapper.Map<RequestPatchCategoryDTO>(category);

        requestPatch.ApplyTo(requestCategoryDTO);

        Validate(requestCategoryDTO);

        return _mapper.Map(requestCategoryDTO!, category!);
    }

    private void Validate(RequestPatchCategoryDTO requestPatch)
    {
        var result = _validation.Validate(requestPatch);

        if (!result.IsValid) 
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
