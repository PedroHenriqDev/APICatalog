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
using Catalog.Application.UseCases;

namespace Application.UseCases.Products.Get;

public class GetProductsByCategoryIdUseCase : UseCase, IGetProductsByCategoryIdUseCase
{
    public GetProductsByCategoryIdUseCase(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<PagedList<ProductDTO>> ExecuteAsync(int categoryId, ProductsParameters productParams)
    {
        EntityValidatorHelper.ValidId(categoryId);

        var products = await _unitOfWork.ProductRepository.GetByCategoryIdAsync(categoryId, productParams);

        EntityValidatorHelper.ValidateEnumerableNotEmpty<Product, NotFoundException>(products, ErrorMessagesResource.PRODUCTS_WITH_CATEGORY_ID_NOT_FOUND.FormatErrorMessage(categoryId));

        return _mapper.Map<PagedList<ProductDTO>>(products);
    }
}
