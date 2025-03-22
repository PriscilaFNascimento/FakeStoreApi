using Domain.Dtos;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace FakeStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCartItemAsync(
            [FromBody] CreateCartItemDto request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _cartItemService.CreateCartItemAsync(request.CostumerId, request, cancellationToken);
                return Created();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "An error occurred while processing your request." });
            }
        }

        [HttpGet("customer/{customerId}")]
        [ProducesResponseType(typeof(IEnumerable<CartItemResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllCartItemsByCustomerIdAsync(
            Guid customerId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var items = await _cartItemService.GetAllCartItemsByCostumerIdAsync(customerId, cancellationToken);
                return Ok(items);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "An error occurred while processing your request." });
            }
        }

        [HttpPut("{cartItemId}/quantity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCartItemQuantityAsync(
            Guid cartItemId,
            [FromBody] int newQuantity,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _cartItemService.UpdateCartItemQuantityAsync(cartItemId, newQuantity, cancellationToken);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "An error occurred while processing your request." });
            }
        }
    }
} 