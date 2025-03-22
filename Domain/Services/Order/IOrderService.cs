using Domain.Dtos;

namespace Domain.Services
{
    public interface IOrderService
    {
        public Task CreateOrderFromCostumerCartAsync(Guid costumerId, CancellationToken cancellationToken = default);
        public Task<IEnumerable<OrderResponseDto>> GetAllOrdersByCostumerId(Guid costumerId, CancellationToken cancellationToken = default);
    }
}
