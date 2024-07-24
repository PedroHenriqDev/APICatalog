﻿using Infrastructure.Domain;
using Communication.DTOs;
using APICatalog.Filters;
using Application.Interfaces;
using Application.Pagination;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Newtonsoft.Json;
using Communication.DTOs.Responses;
using Communication.DTOs;
using Communication.DTOs.Requests;
using Application.Extensions;
using Configuration.Resources;

namespace APICatalog.Controllers;

[ApiController]
[EnableRateLimiting("fixedwindow")]
[EnableCors("allowedorigin")]
[ServiceFilter(typeof(ApiLoggingFilter))]
[Route("api/[controller]")]
[Produces("application/json")]
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
    [Authorize("AdminOnly")]
    [ProducesResponseType(typeof(IEnumerable<ProductDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll([FromQuery] ProductsParameters productsParams)
    {
        var products = await _unitOfWork.ProductRepository.GetProductsAsync(productsParams);

        return GetProductsMetaData(products);
    }

    [HttpGet("filter/price")]
    [ProducesResponseType(typeof(IEnumerable<ProductDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status404NotFound)]
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
    [ProducesResponseType(typeof(IEnumerable<ProductDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetByCategoryIdAsync(int id)
    {
        var products = await _unitOfWork.ProductRepository.GetByCategoryIdAsync(id);
        var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
        
        return Ok(productsDto);
    }

    [HttpGet("withcategory/{id:int:min(1)}")]
    [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDTO>> GetByIdWithCategoryAsync(int id)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdWithCategoryAsync(id);
        var productDto = _mapper.Map<ProductDTO>(product);

        return Ok(productDto);
    }

    [HttpGet("{id:int:min(1)}", Name = "GetProduct")]
    [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDTO>> GetByIdAsync(int id)
    {
        var productDto = _mapper.Map<ProductDTO>(await _unitOfWork.ProductRepository.GetAsync(product => product.ProductId == id));

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

    [HttpPatch("updatepartial/{id:int:min(1)}")]
    public async Task<ActionResult<ResponseProductDTO>> PatchAsync(int id, JsonPatchDocument<RequestProductDTO> productDtoPatch)
    {
        if (productDtoPatch is null || id <= 0)
            return BadRequest();

        var product = await _unitOfWork.ProductRepository.GetAsync(product => product.ProductId == id);

        if (product is null)
            return NotFound($"Not found product id = {id}");

        var productDtoRequest = _mapper.Map<RequestProductDTO>(product);
        productDtoPatch.ApplyTo(productDtoRequest, ModelState);

        if (!ModelState.IsValid || !TryValidateModel(ModelState))
            return BadRequest(ModelState);

        _mapper.Map(productDtoRequest, product);
        _unitOfWork.ProductRepository.Update(product);
        await _unitOfWork.CommitAsync();

        return Ok(_mapper.Map<ResponseProductDTO>(product));
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
            return NotFound(ErrorMessagesResource.PRODUCT_ID_NOT_FOUND.FormatErrorMessage(id));

        _unitOfWork.ProductRepository.Delete(product);
        await _unitOfWork.CommitAsync();

        return Ok(_mapper.Map<ProductDTO>(product));
    }
}
