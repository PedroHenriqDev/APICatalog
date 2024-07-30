using Communication.DTOs.Requests;
using Infrastructure.Domain;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Interfaces.UseCases.Products.Patch;

public interface IPatchProductUseCase
{
    Task<Product> ExecuteAsync(int id, JsonPatchDocument<RequestPatchProductDTO> requestPatch); 
}
