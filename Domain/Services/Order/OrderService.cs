using Domain.Dtos;
using Domain.Entities;
using Domain.Repositories;

namespace Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICostumerRepository _costumerRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IOrderProductRepository _orderProductRepository;

        public OrderService(IOrderRepository orderRepository, ICostumerRepository costumerRepository, ICartItemRepository cartItemRepository, IOrderProductRepository orderProductRepository)
        {
            _orderRepository = orderRepository;
            _costumerRepository = costumerRepository;
            _cartItemRepository = cartItemRepository;
            _orderProductRepository = orderProductRepository;
        }
        public Task<IEnumerable<OrderResponseDto>> GetAllOrdersByCostumerId(Guid costumerId, CancellationToken cancellationToken)
        {
            return _orderRepository.GetAllByCostumerIdAsync(costumerId, cancellationToken);
        }

        public async Task<IEnumerable<OrderProductResponseDto>> GetAllOrderProductsByOrderId(Guid orderId, CancellationToken cancellationToken)
        {
            return await _orderProductRepository.GetByOrderIdAsync(orderId, cancellationToken);
        }

        public async Task CreateOrderFromCostumerCartAsync(Guid costumerId, CancellationToken cancellationToken)
        {
            var costumer = await _costumerRepository.GetByIdAsync(costumerId, cancellationToken);

            //TODO: Implement domain validations and throw a domain exception
            //TODO: Add data annotations to the CreateUpdateCostumerDto instead of doing the validation here
            if (costumer is null)
                throw new ArgumentException("Customer not found");

            var cartItems = await _cartItemRepository.GetAllByCostumerIdAsync(costumerId, cancellationToken);

            if (!cartItems.Any())
                throw new InvalidOperationException("Cart is empty");

            var order = new Order(costumerId);
            await _orderRepository.AddAsync(order, cancellationToken);

            //TODO: Create a extension method to convert CartItem to OrderProduct
            var orderProducts = cartItems.Select(ci => new OrderProduct(order.Id, ci.ProductName,  ci.Quantity, ci.ProductPrice));
            await _orderProductRepository.AddRangeAsync(orderProducts, cancellationToken);

            _cartItemRepository.DeleteAllByCostumerId(costumerId);

            await _orderRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
