using Domain.Dtos;

namespace Domain.Services
{
    public interface ICartItemService
    {
        public Task CreateCartItemAsync(Guid userId, CreateCartItemDto request, CancellationToken cancellationToken = default);
        public Task UpdateCartItemQuantityAsync(Guid cartItemId, int newQuantity, CancellationToken cancellationToken = default);
        public Task<IEnumerable<CartItemResponseDto>> GetAllCartItemsByCostumerIdAsync(Guid costumerIdAsync, CancellationToken cancellationToken = default);
    }
}
