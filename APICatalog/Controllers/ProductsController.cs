using APICatalog.Domain;
using APICatalog.DTOs;
using APICatalog.Filters;
using APICatalog.Interfaces;
using APICatalog.Pagination;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APICatalog.Controllers;

[ServiceFilter(typeof(ApiLoggingFilter))]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll([FromQuery] ProductsParameters productsParams)
    {
        var products = await _unitOfWork.ProductRepository.GetProductsAsync(productsParams);

        if (products is null)
            return NotFound("Products not found");

        return GetProductsMetaData(products);
    }

    [HttpGet("filter/price")]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetFilterPrice([FromQuery] ProductsFilterPriceParameters productsFilterPriceParams) 
    {
        var products = await _unitOfWork.ProductRepository.GetProductsFilterPriceAsync(productsFilterPriceParams);
        return GetProductsMetaData(products);
    }

    [HttpGet]
    private ActionResult<IEnumerable<ProductDTO>> GetProductsMetaData(PagedList<Product> products) 
    {
        var metaData = new
        {
            products.TotalPages,
            products.TotalCount,
            products.PageSize,
            products.CurrentPage,
            products.HasNext,
            products.HasPrevious,
        };

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metaData));

        var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
        return Ok(productsDto);
    }

    [HttpGet("category/{id:int:min(1)}")]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetByCategoryIdAsync(int id)
    {
        var productsDto = _mapper.Map<ProductDTO>(await _unitOfWork.ProductRepository.GetByCategoryIdAsync(id));

        if (productsDto is null)
            return NotFound("Products not found");

        return Ok(productsDto);
    }

    [HttpGet("withcategory/{id:int:min(1)}")]
    public async Task<ActionResult<ProductDTO>> GetByIdWithCategoryAsync(int id)
    {
        var productDto = _mapper.Map<ProductDTO>(await _unitOfWork.ProductRepository.GetByIdWithCategoryAsync(id));

        if (productDto is null)
            return NotFound($"Not found product with id = {id}");

        return Ok(productDto);
    }

    [HttpGet("{id:int:min(1)}", Name = "GetProduct")]
    public async Task<ActionResult<ProductDTO>> GetAsync(int id)
    {
        var productDto = _mapper.Map<ProductDTO>(await _unitOfWork.ProductRepository.GetAsync(product => product.ProductId == id));

        if (productDto is null)
            return NotFound($"Not found product id = {id}");

        return Ok(productDto);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDTO>> PostAsync(Product product)
    {
        if (product is null) if (product is null)
                return NotFound("Product null");

        var createdProduct = await _unitOfWork.ProductRepository.CreateAsync(product);
        await _unitOfWork.CommitAsync();

        var createdProductDto = _mapper.Map<ProductDTO>(createdProduct);
        return new CreatedAtRouteResult("GetProduct", new { id = createdProductDto.ProductId }, createdProductDto);
    }

    [HttpPatch("UpdatePartial/{id:int:min(1)}")]
    public async Task<ActionResult<ProductDTOResponse>> PatchAsync(int id, JsonPatchDocument<ProductDTORequest> productDtoPatch)
    {
        if (productDtoPatch is null || id <= 0)
            return BadRequest();

        var product = await _unitOfWork.ProductRepository.GetAsync(product => product.ProductId == id);

        if (product is null)
            return NotFound($"Not found product id = {id}");

        var productDtoRequest = _mapper.Map<ProductDTORequest>(product);
        productDtoPatch.ApplyTo(productDtoRequest, ModelState);

        if (!ModelState.IsValid || !TryValidateModel(ModelState))
            return BadRequest(ModelState);

        _mapper.Map(productDtoRequest, product);
        _unitOfWork.ProductRepository.Update(product);
        await _unitOfWork.CommitAsync();

        return Ok(_mapper.Map<ProductDTOResponse>(product));
    }

    [HttpPut("{id:int:min(1)}")]
    public async Task<ActionResult<ProductDTO>> PutAsync(int id, Product product)
    {
        if (product.ProductId != id)
            return BadRequest($"Ids entered not matched = {id}");

        _unitOfWork.ProductRepository.Update(product);
        await _unitOfWork.CommitAsync();

        return Ok(_mapper.Map<ProductDTO>(product));
    }

    [HttpDelete("{id:int:min(1)}")]
    public async Task<ActionResult<ProductDTO>> DeleteAsync(int id)
    {
        var product = await _unitOfWork.ProductRepository.GetAsync(product => product.ProductId == id);

        if (product is null)
            return NotFound($"Not found product id = {id}");

        _unitOfWork.ProductRepository.Delete(product);
        await _unitOfWork.CommitAsync();

        return Ok(_mapper.Map<ProductDTO>(product));
    }
}
