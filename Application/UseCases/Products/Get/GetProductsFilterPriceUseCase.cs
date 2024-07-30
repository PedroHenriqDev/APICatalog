using Application.Extensions;
using Application.Interfaces;
using Application.Interfaces.UseCases.Products.Get;
using Application.Pagination;
using Application.Validations;
using AutoMapper;
using Communication.DTOs;
using Configuration.Resources;
using ExceptionManager.ExceptionBase;
using Infrastructure.Domain;

namespace Application.UseCases.Products.Get;

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
