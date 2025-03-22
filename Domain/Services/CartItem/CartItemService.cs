using Domain.Dtos;

namespace Domain.Services
{
    public class CartItemService : ICartItemService
    {
        public Task CreateCartItemAsync(Guid userId, CreateCartItemDto request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CartItemResponseDto>> GetAllCartItemByCostumerIdAsync(Guid costumerIdAsync, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCartItemQuantityAsync(Guid cartItemId, int newQuantity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
