using Catalog.Application.Extensions;
using Catalog.Application.Interfaces;
using Catalog.Application.Interfaces.UseCases.Products.Patch;
using Catalog.Application.Validations;
using Catalog.Application.Validations.Products;
using AutoMapper;
using Catalog.Communication.DTOs.Requests;
using Catalog.Configuration.Resources;
using Catalog.ExceptionManager.ExceptionBase;
using Catalog.Domain;
using Microsoft.AspNetCore.JsonPatch;

namespace Catalog.Application.UseCases.Products.Patch;

public class PatchProductUseCase : UseCase, IPatchProductUseCase
{
    private readonly RequestProductDTOValidation _validation;

    public PatchProductUseCase(IUnitOfWork unitOfWork, IMapper mapper, RequestProductDTOValidation validation) : base(unitOfWork, mapper)
    {
        _validation = validation;
    }

    public async Task<Product> ExecuteAsync(int id, JsonPatchDocument<RequestPatchProductDTO> requestPatch) 
    {
        var product = await _unitOfWork.ProductRepository.GetAsync(product => product.ProductId == id);

        EntityValidatorHelper.ValidateNotNull<Product, NotFoundException>(product, ErrorMessagesResource.PRODUCT_ID_NOT_FOUND.FormatErrorMessage(id));
        
        var requestProductDTO = _mapper.Map<RequestPatchProductDTO>(product);

        requestPatch.ApplyTo(requestProductDTO);

        Validate(requestProductDTO);

        return _mapper.Map(requestProductDTO!, product!);
    }

    private void Validate(RequestPatchProductDTO requestPatch)
    {
        var result = _validation.Validate(requestPatch);

        if(!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
