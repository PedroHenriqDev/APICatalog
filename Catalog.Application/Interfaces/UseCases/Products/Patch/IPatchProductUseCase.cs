using Catalog.Communication.DTOs.Requests;
using Catalog.Domain;
using Microsoft.AspNetCore.JsonPatch;

namespace Catalog.Application.Interfaces.UseCases.Products.Patch;

public interface IPatchProductUseCase
{
    Task<Product> ExecuteAsync(int id, JsonPatchDocument<RequestPatchProductDTO> requestPatch); 
}
