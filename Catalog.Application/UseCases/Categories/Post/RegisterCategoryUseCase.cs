using Catalog.Application.Interfaces;
using Catalog.Application.Interfaces.UseCases.Categories.Post;
using Catalog.Application.Validations.Categories;
using AutoMapper;
using Catalog.Communication.DTOs;
using Catalog.ExceptionManager.ExceptionBase;
using Catalog.Domain;

namespace Catalog.Application.UseCases.Categories.Post;

public class RegisterCategoryUseCase : UseCase, IRegisterCategoryUseCase
{
    private readonly CategoryValidation _validation;

    public RegisterCategoryUseCase(IUnitOfWork unitOfWork, 
                                   IMapper mapper,
                                   CategoryValidation validation) : base(unitOfWork, mapper)
    {
        _validation = validation;
    }

    public async Task<CategoryDTO> ExecuteAsync(CategoryDTO categoryDTO) 
    {
        Validate(categoryDTO);

        var category = await _unitOfWork.CategoryRepository.CreateAsync(_mapper.Map<Category>(categoryDTO));
        await _unitOfWork.CommitAsync();

        return _mapper.Map<CategoryDTO>(category);
    }

    private void Validate(CategoryDTO categoryDTO) 
    {
        var result = _validation.Validate(categoryDTO);

        if(!result.IsValid) 
        {
            var errorsMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            
            throw new ErrorOnValidationException(errorsMessages);
        }
    }
}
