using Catalog.Application.Extensions;
using Catalog.Application.Interfaces;
using Catalog.Application.Interfaces.UseCases.Categories.Delete;
using Catalog.Application.Interfaces.UseCases.Products.Delete;
using Catalog.Application.Validations;
using AutoMapper;
using Catalog.Communication.DTOs;
using Catalog.Configuration.Resources;
using Catalog.ExceptionManager.ExceptionBase;
using Catalog.Domain;

namespace Catalog.Application.UseCases.Products.Delete;

public class DeleteProductByIdUseCase : UseCase, IDeleteProductByIdUseCase
{
    public DeleteProductByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<ProductDTO> ExecuteAsync(int id) 
    {
        EntityValidatorHelper.ValidId(id);

        var productToDelete = await _unitOfWork.ProductRepository.GetAsync(product => product.ProductId == id);

        EntityValidatorHelper.ValidateNotNull<Product, NotFoundException>(productToDelete, ErrorMessagesResource.PRODUCT_ID_NOT_FOUND.FormatErrorMessage(id));
   
        var productDeleted = _unitOfWork.ProductRepository.Delete(productToDelete!);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<ProductDTO>(productDeleted);
    }
}
