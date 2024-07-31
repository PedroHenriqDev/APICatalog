using Catalog.Communication.DTOs;
using Catalog.API.Filters;
using Catalog.Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Newtonsoft.Json;
using Catalog.Communication.DTOs.Responses;
using Catalog.Communication.DTOs.Requests;
using AutoMapper;
using Catalog.Application.Interfaces.Managers;

namespace Catalog.API.Controllers;

[ApiController]
[ServiceFilter(typeof(ApiLoggingFilter))]
[Route("api/[controller]")]
[EnableRateLimiting("fixedwindow")]
[EnableCors("allowedorigin")]
[Produces("application/json")]
public class CategoriesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICategoryUseCaseManager _useCaseManager;

    public CategoriesController(IMapper mapper, ICategoryUseCaseManager useCaseManager)
    {
        _mapper = mapper;
        _useCaseManager = useCaseManager;
    }

    [HttpGet]
    [Route("all")]
    [Authorize(policy: "AdminOnly")]
    [ProducesResponseType(typeof(PagedList<CategoryDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllAsync([FromQuery] CategoriesParameters categoriesParams)
    {
        var categoriesDTO = await _useCaseManager.GetProvider.GetUseCase.ExecuteAsync(categoriesParams);
       
        return GetCategoriesMetaData(categoriesDTO);
    }

    [HttpGet]
    [Route("withproducts/{id:int:min(1)}")]
    [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDTO>> GetByIdWithProductsAsync([FromRoute] int id)
    {
        var categoryDTO = await _useCaseManager.GetProvider.GetByIdWithProductsUseCase.ExecuteAsync(id);

        return Ok(categoryDTO);
    }

    [HttpGet]
    [Route("stats")]
    [ProducesResponseType(typeof(ResponseCategoriesStatsDTO), StatusCodes.Status200OK)]
    public async Task<ActionResult<ResponseCategoriesStatsDTO>> GetStatsAsync([FromQuery] CategoriesParameters categoriesParams)
    {
        var statsDTO = await _useCaseManager.GetProvider.GetStatsUseCase.ExecuteAsync(categoriesParams);
        return Ok(statsDTO);
    }

    [HttpGet]
    [Route("filter/name")]
    [ProducesResponseType(typeof(PagedList<CategoryDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesFilterNameAsync([FromQuery] CategoriesFilterNameParameters categoriesNameParams)
    {
        var categoriesDTO = await _useCaseManager.GetProvider.GetFilterNameUseCase.ExecuteAsync(categoriesNameParams);
        return GetCategoriesMetaData(categoriesDTO);
    }

    [HttpGet("{id:int:min(1)}", Name = "GetCategory")]
    [Authorize(policy: "UserOnly")]
    [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDTO>> GetByIdAsync([FromRoute] int id)
    {
        var categoryDTO = await _useCaseManager.GetProvider.GetByIdUseCase.ExecuteAsync(id);
        return Ok(categoryDTO);
    }

    private ActionResult<IEnumerable<CategoryDTO>> GetCategoriesMetaData(PagedList<CategoryDTO> categoriesDTO)
    {
        var metaData = new
        {
            categoriesDTO.PageSize,
            categoriesDTO.TotalCount,
            categoriesDTO.Count,
            categoriesDTO.TotalPages,
            categoriesDTO.HasNext,
            categoriesDTO.HasPrevious,
            categoriesDTO.CurrentPage
        };

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metaData));
        return Ok(categoriesDTO);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoryDTO>> PostAsync(CategoryDTO categoryDTO)
    {
        var createdCategoryDTO = await _useCaseManager.PostProvider.RegisterUseCase.ExecuteAsync(categoryDTO);
        return new CreatedAtRouteResult("GetCategory", new { id = createdCategoryDTO.CategoryId }, createdCategoryDTO);
    }

    [HttpPatch]
    [Route("updatepartial/{id:int:min(1)}")]
    [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDTO>> PatchAsync([FromRoute] int id, JsonPatchDocument<RequestPatchCategoryDTO> patchRequest)
    {
        var category = await _useCaseManager.PatchProvider.PatchUseCase.ExecuteAsync(id, patchRequest);

        if (!ModelState.IsValid || !TryValidateModel(ModelState))
            return BadRequest(ModelState);

        var categoryDTO = await _useCaseManager.PutProvider.UpdateUseCase.ExecuteAsync(id, _mapper.Map<CategoryDTO>(category));
        return Ok(categoryDTO);
    }

    [HttpPut("{id:int:min(1)}")]
    [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CategoryDTO>> PutAsync([FromRoute] int id, CategoryDTO categoryDTO)
    {
        var category = await _useCaseManager.PutProvider.UpdateUseCase.ExecuteAsync(id, categoryDTO);

        return Ok(category);
    }

    [HttpDelete("{id:int:min(1)}")]
    [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDTO>> DeleteAsnyc([FromRoute] int id)
    {
        var categoryDTO = await _useCaseManager.DeleteProvider.DeleteByIdUseCase.ExecuteAsync(id);
        return Ok(categoryDTO);
    }
}
