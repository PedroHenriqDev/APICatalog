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
