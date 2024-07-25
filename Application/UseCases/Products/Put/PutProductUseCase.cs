using Application.Extensions;
using Application.Interfaces;
using Application.Validations;
using Application.Validations.Products;
using AutoMapper;
using Communication.DTOs;
using Configuration.Resources;
using ExceptionManager.ExceptionBase;
using Infrastructure.Domain;

namespace Application.UseCases.Products.Put;

public class PutProductUseCase : UseCase
{
    private readonly ProductValidation _validation;

    public PutProductUseCase(IUnitOfWork unitOfWork, IMapper mapper, ProductValidation validation) : base(unitOfWork, mapper)
    {
        _validation = validation;
    }

    public async Task<ProductDTO> ExecuteAsync(int id, ProductDTO productDTO) 
    {
        Validate(productDTO);

        EntityValidatorHelper.ValidId(id);

        var productExisting = await _unitOfWork.ProductRepository.GetAsync(product => product.ProductId == id);

        EntityValidatorHelper.ValidateNotNull<Product, NotFoundException>(productExisting, ErrorMessagesResource.PRODUCT_ID_NOT_FOUND.FormatErrorMessage(id));

        var productToUpdate = _mapper.Map(productDTO, productExisting);

        var updatedProduct = _unitOfWork.ProductRepository.Update(productToUpdate!);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<ProductDTO>(updatedProduct);
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
