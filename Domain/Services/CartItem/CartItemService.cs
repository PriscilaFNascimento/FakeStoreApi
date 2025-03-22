using Domain.Dtos;
using Domain.Entities;
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

        public async Task CreateCartItemAsync(Guid userId, CreateCartItemDto request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request), "request cannot be null");

            if (string.IsNullOrWhiteSpace(request.ProductName))
                throw new ArgumentException("ProductName cannot be null, empty or whitespace", nameof(request));

            if (request.ProductPrice <= 0)
                throw new ArgumentException("ProductPrice must be greater than zero", nameof(request));

            var existingItem = await _cartItemRepository.GetByCostumerIdAndProductNameAsync(userId, request.ProductName, cancellationToken);

            if (existingItem is null)
            {
                var cartItem = new CartItem(userId, request.ProductName, request.ProductPrice);
                await _cartItemRepository.AddAsync(cartItem, cancellationToken);
            }
            else
            {
                existingItem.Quantity++;
                await _cartItemRepository.UpdateAsync(existingItem, cancellationToken);
            }

            await _cartItemRepository.SaveChangesAsync(cancellationToken);
        }

        public Task<IEnumerable<CartItemResponseDto>> GetAllCartItemsByCostumerIdAsync(Guid costumerIdAsync, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCartItemQuantityAsync(Guid cartItemId, int newQuantity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
