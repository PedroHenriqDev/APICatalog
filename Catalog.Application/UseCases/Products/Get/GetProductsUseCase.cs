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

public class GetProductsUseCase : UseCase, IGetProductsUseCase
{
    public GetProductsUseCase(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<PagedList<ProductDTO>> ExecuteAsync(ProductsParameters productParams) 
    {
        var products = await _unitOfWork.ProductRepository.GetProductsAsync(productParams);

        EntityValidatorHelper.ValidateEnumerableNotEmpty<Product, NotFoundException>(products, ErrorMessagesResource.NOT_FOUND) ;

        return _mapper.Map<PagedList<ProductDTO>>(products);
    }
}
