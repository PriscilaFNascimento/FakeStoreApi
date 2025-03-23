using Domain.Dtos;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace FakeStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductResponseDto>), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAllProducts(CancellationToken cancellationToken)
        {
            try
            {
                var products = await _productService.GetAllProductsAsync(cancellationToken);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductResponseDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ProductResponseDto>> GetProductById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id, cancellationToken);
                if (product == null)
                {
                    return NotFound(new { message = $"Product with ID {id} not found" });
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
} 