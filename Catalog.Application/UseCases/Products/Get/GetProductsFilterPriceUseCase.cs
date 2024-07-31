using Catalog.Application.Extensions;
using Catalog.Application.Interfaces;
using Catalog.Application.Interfaces.UseCases.Products.Get;
using Catalog.Application.Pagination;
using Catalog.Application.Validations;
using AutoMapper;
using Catalog.Communication.DTOs;
using Catalog.Configuration.Resources;
using Catalog.ExceptionManager.ExceptionBase;
using Catalog.Domain;

namespace Catalog.Application.UseCases.Products.Get;

public class GetProductsFilterPriceUseCase : UseCase, IGetProductsFilterPriceUseCase
{
    public GetProductsFilterPriceUseCase(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<PagedList<ProductDTO>> ExecuteAsync(ProductsFilterPriceParameters productParams) 
    {
        var products = await _unitOfWork.ProductRepository.GetProductsFilterPriceAsync(productParams);

        EntityValidatorHelper.ValidateEnumerableNotEmpty<Product, NotFoundException>(products, ErrorMessagesResource.PRODUCTS_PRICE_NOT_FOUND.FormatErrorMessage(productParams.Price));

        return _mapper.Map<PagedList<ProductDTO>>(products);
    }
}
