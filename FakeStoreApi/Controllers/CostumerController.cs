using Domain.Dtos;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace FakeStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CostumerController : ControllerBase
    {
        private readonly ICostumerService _costumerService;

        public CostumerController(ICostumerService costumerService)
        {
            _costumerService = costumerService;
        }

        ///Creating and updating should be two different endpoints, but for the sake of simplicity, I'm using the same endpoint.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrUpdateCostumerAsync(
            [FromBody] CreateUpdateCostumerDto request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _costumerService.CreateOrUpdateCostumerAsync(request, cancellationToken);
                return Ok();
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
    }
} 