using Catalog.Communication.DTOs.Requests;
using Catalog.Domain;
using Microsoft.AspNetCore.JsonPatch;

namespace Catalog.Application.Interfaces.UseCases.Categories.Patch;

public interface IPatchCategoryUseCase
{
    Task<Category> ExecuteAsync(int id, JsonPatchDocument<RequestPatchCategoryDTO> requestPatch);
}
