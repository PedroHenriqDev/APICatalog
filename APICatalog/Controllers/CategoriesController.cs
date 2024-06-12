using APICatalog.Domain;
using APICatalog.Filters;
using APICatalog.Interfaces;
using APICatalog.Repositories;
using APICatalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace APICatalog.Controllers
{
    [ServiceFilter(typeof(ApiLoggingFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("UsingFromServices/{name}")]
        public ActionResult<string> GetSalutationFromServices([FromServices] ISalutationService salutationService, string name)
        {
            return salutationService.Salutation(name);
        }

        [HttpGet("WithoutFromServices/{name}")]
        public ActionResult<string> GetSalutationWithoutFromServices(ISalutationService salutationService, string name)
        {
            return salutationService.Salutation(name);
        }

        [HttpGet("withproducts")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllWithProductsAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllWithProductsAsync();
            if(categories is null) 
            {
                return NotFound("Categories not found");
            }
            return Ok(categories);
        }

        [HttpGet("withproducts/{id:int:min(1)}")]
        public async Task<ActionResult<Category>> GetByIdWithProductsAsync(int id)
        {
            return Ok(await _unitOfWork.CategoryRepository.GetByIdWithProductsAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();

            if (categories is null)
            {
                return NotFound("Categories not found!");
            }

            return Ok(categories);
        }

        [HttpGet("{id:int:min(1)}", Name = "GetCategory")]
        public async Task<ActionResult<Category>> GetAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetAsync(category => category.CategoryId == id);
            if (category is null)
            {
                return NotFound($"No category found with id = {id}");
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(Category category)
        {
            if (category is null)
            {
                return BadRequest("Category null");
            }

            await _unitOfWork.CategoryRepository.CreateAsync(category);
            await _unitOfWork.CommitAsync();
            return new CreatedAtRouteResult("GetCategory", new { id = category.CategoryId }, category);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult<Category>> PutAsync(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest($"Ids entered do not match= {id}");
            }

            await _unitOfWork.CategoryRepository.UpdateAsync(category);
            await _unitOfWork.CommitAsync();
            return Ok(category);
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult<Category>> DeleteAsnyc(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetAsync(category => category.CategoryId == id);
            if (category is null)
            {
                return NotFound($"Not found category id = {id}");
            }

            await _unitOfWork.CategoryRepository.DeleteAsync(category);
            await _unitOfWork.CommitAsync();
            return Ok(category);
        }
    }
}
