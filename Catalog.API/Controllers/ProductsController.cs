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
[EnableRateLimiting("fixedwindow")]
[EnableCors("allowedorigin")]
[ServiceFilter(typeof(ApiLoggingFilter))]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IProductUseCaseManager _useCaseManager;

    public ProductsController(IMapper mapper, IProductUseCaseManager useCaseManager)
    {
        _mapper = mapper;
        _useCaseManager = useCaseManager;
    }

    [HttpGet]
    [Authorize("AdminOnly")]
    [ProducesResponseType(typeof(IEnumerable<ProductDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll([FromQuery] ProductsParameters productsParams)
    {
        var productsDTO = await _useCaseManager.GetProvider.GetUseCase.ExecuteAsync(productsParams);

        return GetProductsMetaData(productsDTO);
    }

    [HttpGet("{id:int:min(1)}", Name = "GetProduct")]
    [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDTO>> GetByIdAsync([FromRoute] int id)
    {
        var productDTO = await _useCaseManager.GetProvider.GetByIdUseCase.ExecuteAsync(id);

        return Ok(productDTO);
    }

    [HttpGet("filter/price")]
    [ProducesResponseType(typeof(IEnumerable<ProductDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetFilterPrice([FromQuery] ProductsFilterPriceParameters productsPriceParams)
    {
        var products = await _useCaseManager.GetProvider.GetFilterPriceUseCase.ExecuteAsync(productsPriceParams);
        return GetProductsMetaData(products);
    }

    [HttpGet("category/{id:int:min(1)}")]
    [ProducesResponseType(typeof(IEnumerable<ProductDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetByCategoryIdAsync([FromRoute] int id, [FromQuery] ProductsParameters productParams)
    {
        var productsDTO = await _useCaseManager.GetProvider.GetByCategoryIdUseCase.ExecuteAsync(id, productParams);

        var okResult = GetProductsMetaData(productsDTO);

        return okResult;
    }

    [HttpGet("withcategory/{id:int:min(1)}")]
    [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDTO>> GetByIdWithCategoryAsync([FromRoute] int id)
    {
        var productDTO = await _useCaseManager.GetProvider.GetByIdWithCategoryUseCase.ExecuteAsync(id);

        return Ok(productDTO);
    }

    [NonAction]
    public ActionResult<IEnumerable<ProductDTO>> GetProductsMetaData(PagedList<ProductDTO> productsDTO)
    {
        var metaData = new
        {
            productsDTO.TotalPages,
            productsDTO.TotalCount,
            productsDTO.PageSize,
            productsDTO.CurrentPage,
            productsDTO.HasNext,
            productsDTO.HasPrevious,
        };

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metaData));

        return Ok(productsDTO);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDTO>> PostAsync([FromBody] ProductDTO productDTO)
    {
        var createdProductDTO = await _useCaseManager.PostProvider.RegisterUseCase.ExecuteAsync(productDTO);

        return new CreatedAtRouteResult("GetProduct", new { id = createdProductDTO.ProductId }, createdProductDTO);
    }

    [HttpPatch("updatepartial/{id:int:min(1)}")]
    [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDTO>> PatchAsync([FromRoute]int id, JsonPatchDocument<RequestPatchProductDTO> productDtoPatch)
    {
        var product = await _useCaseManager.PatchProvider.PatchUseCase.ExecuteAsync(id, productDtoPatch);

        if (!ModelState.IsValid || !TryValidateModel(ModelState))
            return BadRequest(ModelState);

        var updateProduct = await _useCaseManager.PutProvider.PutUseCase.ExecuteAsync(id, _mapper.Map<ProductDTO>(product));

        return Ok(updateProduct);
    }

    [HttpPut("{id:int:min(1)}")]
    [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDTO>> PutAsync([FromRoute] int id, [FromBody] ProductDTO productDTO)
    {
       var updatedProductDTO = await _useCaseManager.PutProvider.PutUseCase.ExecuteAsync(id, productDTO);

        return Ok(updatedProductDTO);
    }

    [HttpDelete("{id:int:min(1)}")]
    [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDTO>> DeleteAsync([FromRoute] int id)
    {
        var deletedProductDTO = await _useCaseManager.DeleteProvider.DeleteByIdUseCase.ExecuteAsync(id);
        
        return Ok(deletedProductDTO);
    }
}
