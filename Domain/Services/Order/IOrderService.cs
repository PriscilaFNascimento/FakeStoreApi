using Domain.Dtos;

namespace Domain.Services
{
    public interface IOrderService
    {
        public Task<IEnumerable<OrderResponseDto>> GetAllOrdersByCostumerId(Guid costumerId, CancellationToken cancellationToken);
        public Task<IEnumerable<OrderProductResponseDto>> GetAllOrderProductsByOrderId(Guid orderId, CancellationToken cancellationToken);
        public Task CreateOrderFromCostumerCartAsync(Guid costumerId, CancellationToken cancellationToken);
    }
}
