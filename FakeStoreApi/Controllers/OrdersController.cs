using Domain.Dtos;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace FakeStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("customer/{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetOrdersByCustomerId(
            Guid customerId,
            CancellationToken cancellationToken)
        {
            try
            {
                var orders = await _orderService.GetAllOrdersByCostumerId(customerId, cancellationToken);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while retrieving orders", error = ex.Message });
            }
        }

        [HttpGet("{orderId}/products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OrderProductResponseDto>>> GetOrderProductsByOrderId(
            Guid orderId,
            CancellationToken cancellationToken)
        {
            try
            {
                var orders = await _orderService.GetAllOrderProductsByOrderId(orderId, cancellationToken);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while retrieving orders", error = ex.Message });
            }
        }



        [HttpPost("customer/{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrderFromCart(
            Guid customerId,
            CancellationToken cancellationToken)
        {
            try
            {
                await _orderService.CreateOrderFromCostumerCartAsync(customerId, cancellationToken);
                return Ok(new { message = "Order created successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while creating the order", error = ex.Message });
            }
        }
    }
} 