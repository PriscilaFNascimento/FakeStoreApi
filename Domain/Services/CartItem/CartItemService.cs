using Domain.Dtos;
using Domain.Repositories;

namespace Domain.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;
        public CartItemService(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public Task<IEnumerable<CartItemResponseDto>> GetAllCartItemsByCostumerIdAsync(Guid costumerIdAsync, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task CreateCartItemAsync(Guid userId, CreateCartItemDto request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public Task UpdateCartItemQuantityAsync(Guid cartItemId, int newQuantity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
