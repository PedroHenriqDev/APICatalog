using Application.Extensions;
using Application.Interfaces;
using Application.Interfaces.UseCases.Categories.Delete;
using Application.Interfaces.UseCases.Products.Delete;
using Application.Validations;
using AutoMapper;
using Communication.DTOs;
using Configuration.Resources;
using ExceptionManager.ExceptionBase;
using Infrastructure.Domain;

namespace Application.UseCases.Products.Delete;

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
