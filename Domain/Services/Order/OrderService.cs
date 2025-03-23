using Domain.Dtos;
using Domain.Repositories;

namespace Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICostumerRepository _costumerRepository;
        private readonly ICartItemRepository _cartItemRepository;

        public OrderService(IOrderRepository orderRepository, ICostumerRepository costumerRepository, ICartItemRepository cartItemRepository)
        {
            _orderRepository = orderRepository;
            _costumerRepository = costumerRepository;
            _cartItemRepository = cartItemRepository;
        }
        public Task<IEnumerable<OrderResponseDto>> GetAllOrdersByCostumerId(Guid costumerId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task CreateOrderFromCostumerCartAsync(Guid costumerId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
