using Application.Extensions;
using Application.Interfaces;
using Application.Pagination;
using Application.Validations;
using AutoMapper;
using Communication.DTOs;
using Configuration.Resources;
using ExceptionManager.ExceptionBase;
using Infrastructure.Domain;

namespace Application.UseCases.Products.Get;

public class GetProductsByCategoryIdUseCase : UseCase
{
    public GetProductsByCategoryIdUseCase(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<PagedList<ProductDTO>> ExecuteAsync(int categoryId)
    {
        EntityValidatorHelper.ValidId(categoryId);

        var products = await _unitOfWork.ProductRepository.GetByCategoryIdAsync(categoryId);

        EntityValidatorHelper.ValidateEnumerableNotEmpty<Product, NotFoundException>(products, ErrorMessagesResource.PRODUCTS_WITH_CATEGORY_ID_NOT_FOUND.FormatErrorMessage(categoryId));

        return _mapper.Map<PagedList<ProductDTO>>(products);
    }
}
