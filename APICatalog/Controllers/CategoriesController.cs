using Infrastructure.Domain;
using Application.DTOs;
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

namespace APICatalog.Controllers;

[ApiController]
[ServiceFilter(typeof(ApiLoggingFilter))]
[Route("api/[controller]")]
[EnableRateLimiting("fixedwindow")]
[EnableCors("allowedorigin")]
[Produces("application/json")]
public class CategoriesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("withproducts/{id:int:min(1)}")]
    public async Task<ActionResult<CategoryDTO>> GetByIdWithProductsAsync(int id)
    {
        var categoryDto = _mapper.Map<CategoryDTO>(await _unitOfWork.CategoryRepository.GetByIdWithProductsAsync(id));
        
        if (categoryDto is null)
            return NotFound($"Not found category with id = {id}");

        return Ok(categoryDto);
    }

    [HttpGet]
    [Authorize("AdminOnly")]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllAsync([FromQuery] CategoriesParameters categoriesParams)
    {
        var categories = await _unitOfWork.CategoryRepository.GetCategoriesAsync(categoriesParams);
        
        if (categories is null) 
            return NotFound("Categories not found!");

        return GetCategoriesMetaData(categories);
    }

    [HttpGet("filter/name")]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesFilterNameAsync([FromQuery] CategoriesFilterNameParameters categoriesFilterNameParams) 
    {
        var categories = await _unitOfWork.CategoryRepository.GetCategoriesFilterNameAsync(categoriesFilterNameParams);
        return GetCategoriesMetaData(categories);
    }
        
    [HttpGet]
    private ActionResult<IEnumerable<CategoryDTO>> GetCategoriesMetaData(PagedList<Category> categories) 
    {
        var metaData = new
        {
            categories.PageSize,
            categories.TotalCount,
            categories.Count,
            categories.TotalPages,
            categories.HasNext,
            categories.HasPrevious,
            categories.CurrentPage
        };

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metaData));

        return Ok(_mapper.Map<IEnumerable<CategoryDTO>>(categories));
    }

    [HttpGet("{id:int:min(1)}", Name = "GetCategory")]
    [Authorize(policy: "UserOnly")]
    public async Task<ActionResult<CategoryDTO>> GetAsync(int id)
    {
        var categoryDto = _mapper.Map<CategoryDTO>(await _unitOfWork.CategoryRepository.GetAsync(category => category.CategoryId == id));
        
        if (categoryDto is null)
            return NotFound($"No category found with id = {id}");

        return Ok(categoryDto);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDTO>> PostAsync(CategoryDTO categoryDto)
    {
        if (categoryDto is null)
            return BadRequest("Category null");

        var createdCategory = await _unitOfWork.CategoryRepository.CreateAsync(_mapper.Map<Category>(categoryDto));
        await _unitOfWork.CommitAsync();
        
        var createdCategoryDto = _mapper.Map<CategoryDTO>(createdCategory);
       
        return new CreatedAtRouteResult("GetCategory", new { id = createdCategoryDto.CategoryId }, createdCategoryDto);
    }

    [HttpPatch("updatepartial/{id:int:min(1)}")]
    public async Task<ActionResult<CategoryDTOResponse>> PatchAsync(int id, JsonPatchDocument<CategoryDTORequest> categoryDtoPatch)
    {
        if(categoryDtoPatch is null || id < 0) 
            return BadRequest();

        var category = await _unitOfWork.CategoryRepository.GetAsync(category => category.CategoryId == id);           
        
        if(category is null)
            return BadRequest();

        var categoryDtoRequest = _mapper.Map<CategoryDTORequest>(category);
        categoryDtoPatch.ApplyTo(categoryDtoRequest);

        if (!ModelState.IsValid || !TryValidateModel(ModelState))
            return BadRequest(ModelState);

        _mapper.Map(categoryDtoRequest, category);
        _unitOfWork.CategoryRepository.Update(category);
        await _unitOfWork.CommitAsync();

        return Ok(_mapper.Map<CategoryDTOResponse>(category));
    }

    [HttpPut("{id:int:min(1)}")]
    public async Task<ActionResult<CategoryDTO>> PutAsync(int id, CategoryDTO categoryDto)
    {
        if (id != categoryDto.CategoryId)
            return BadRequest($"Ids entered do not match= {id}");

        _unitOfWork.CategoryRepository.Update(_mapper.Map<Category>(categoryDto));
        await _unitOfWork.CommitAsync();
       
        return Ok(categoryDto);
    }

    [HttpDelete("{id:int:min(1)}")]
    public async Task<ActionResult<CategoryDTO>> DeleteAsnyc(int id)
    {
        var category = await _unitOfWork.CategoryRepository.GetAsync(category => category.CategoryId == id);
        
        if (category is null)
            return NotFound($"Not found category id = {id}");

        _unitOfWork.CategoryRepository.Delete(category);
        await _unitOfWork.CommitAsync();
        
        return Ok(_mapper.Map<CategoryDTO>(category));
    }
}
