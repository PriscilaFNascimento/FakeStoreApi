using Domain.Dtos;

namespace Domain.Services
{
    public class OrderService : IOrderService
    {
        public Task CreateOrderFromCostumerCartAsync(Guid costumerId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderResponseDto>> GetAllOrdersByCostumerId(Guid costumerId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
