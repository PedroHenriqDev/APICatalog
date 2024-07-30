using Communication.DTOs.Requests;
using Infrastructure.Domain;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Interfaces.UseCases.Categories.Patch;

public interface IPatchCategoryUseCase
{
    Task<Category> ExecuteAsync(int id, JsonPatchDocument<RequestPatchCategoryDTO> requestPatch);
}
