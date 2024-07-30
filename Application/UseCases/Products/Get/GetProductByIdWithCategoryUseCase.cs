using Application.Extensions;
using Application.Interfaces;
using Application.Interfaces.UseCases.Products.Get;
using Application.Validations;
using AutoMapper;
using Communication.DTOs;
using Configuration.Resources;
using ExceptionManager.ExceptionBase;
using Infrastructure.Domain;

namespace Application.UseCases.Products.Get;

public class GetProductByIdWithCategoryUseCase : UseCase, IGetProductByIdWithCategoryUseCase
{
    public GetProductByIdWithCategoryUseCase(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<ProductDTO> ExecuteAsync(int id) 
    {
        EntityValidatorHelper.ValidId(id);

        var product = await _unitOfWork.ProductRepository.GetByIdWithCategoryAsync(id);

        EntityValidatorHelper.ValidateNotNull<Product, NotFoundException>(product, ErrorMessagesResource.PRODUCT_ID_NOT_FOUND.FormatErrorMessage(id));

        return _mapper.Map<ProductDTO>(product);
    }
}
