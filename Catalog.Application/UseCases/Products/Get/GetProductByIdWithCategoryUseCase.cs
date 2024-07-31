using Catalog.Application.Extensions;
using Catalog.Application.Interfaces;
using Catalog.Application.Interfaces.UseCases.Products.Get;
using Catalog.Application.Validations;
using AutoMapper;
using Catalog.Communication.DTOs;
using Catalog.Configuration.Resources;
using Catalog.ExceptionManager.ExceptionBase;
using Catalog.Domain;

namespace Catalog.Application.UseCases.Products.Get;

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
