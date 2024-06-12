using APICatalog.Domain;
using APICatalog.Filters;
using APICatalog.Interfaces;
using APICatalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APICatalog.Controllers
{
    [ServiceFilter(typeof(ApiLoggingFilter))]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
            
        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();
            if (products is null)
            {
                return NotFound("Products not found");
            }
            return Ok(products);
        }

        [HttpGet("categoryid/{id:int:min(1)}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetByCategoryIdAsync(int id) 
        {
            var products = await _unitOfWork.ProductRepository.GetByCategoryIdAsync(id);
            if (products is null)
            {
                return NotFound("Products not found");
            }
            return Ok(products);
        }

        [HttpGet("withcategory")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllWithCategoryAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllWithCategoryAsync();
            if (products is null)
            {
                return NotFound("Products not found");
            }

            return Ok(products);
        }

        [HttpGet("withcategory/{id:int:min(1)}")]
        public async Task<ActionResult<Product>> GetByIdWithCategoryAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdWithCategoryAsync(id);
            if (product is null)
            {
                return NotFound($"Not found product with id = {id}");
            }

            return Ok(product);
        }

        [HttpGet("{id:int:min(1)}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> GetAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetAsync(product => product.ProductId == id);
            if (product is null)
            {
                return NotFound($"Not found product id = {id}");
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(Product product)
        {
                                                                                                                                                                                                                         if (product is null)
            {
                return NotFound("Product null");
            }

            await _unitOfWork.ProductRepository.CreateAsync(product);
            await _unitOfWork.CommitAsync();
            return new CreatedAtRouteResult("GetProduct", new { id = product.ProductId }, product);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult<Product>> PutAsync(int id, Product product)
        {
            if (product.ProductId != id)
            {
                return BadRequest($"Ids entered not matched = {id}");
            }

            await _unitOfWork.ProductRepository.UpdateAsync(product);
            await _unitOfWork.CommitAsync();
            return Ok(product);
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult<Product>> DeleteAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetAsync(product => product.ProductId == id);
            if (product is null)
            {
                return NotFound($"Not found product id = {id}");
            }

            await _unitOfWork.ProductRepository.DeleteAsync(product);
            await _unitOfWork.CommitAsync();
            return Ok();
        }
    }
}
