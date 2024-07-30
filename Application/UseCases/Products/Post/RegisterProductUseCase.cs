using Application.Interfaces;
using Application.Interfaces.UseCases.Products.Post;
using Application.Validations.Products;
using AutoMapper;
using Communication.DTOs;
using ExceptionManager.ExceptionBase;
using Infrastructure.Domain;

namespace Application.UseCases.Products.Post;

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
