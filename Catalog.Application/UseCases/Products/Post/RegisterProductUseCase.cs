using Catalog.Application.Interfaces;
using Catalog.Application.Interfaces.UseCases.Products.Post;
using Catalog.Application.Validations.Products;
using AutoMapper;
using Catalog.Communication.DTOs;
using Catalog.ExceptionManager.ExceptionBase;
using Catalog.Domain;

namespace Catalog.Application.UseCases.Products.Post;

public class RegisterProductUseCase : UseCase, IRegisterProductUseCase
{
    private readonly ProductValidation _validation;

    public RegisterProductUseCase(IUnitOfWork unitOfWork, IMapper mapper, ProductValidation validation) : base(unitOfWork, mapper)
    {
        _validation = validation;
    }

    public async Task<ProductDTO> ExecuteAsync(ProductDTO productDTO) 
    {
        Validate(productDTO);

        var product = await _unitOfWork.ProductRepository.CreateAsync(_mapper.Map<Product>(productDTO));
        await _unitOfWork.CommitAsync();

        return _mapper.Map<ProductDTO>(product);
    }

    private void Validate(ProductDTO productDTO) 
    {
        var result = _validation.Validate(productDTO);

        if (!result.IsValid) 
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
