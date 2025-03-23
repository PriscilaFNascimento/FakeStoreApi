using Domain.Dtos;
using Domain.Entities;
using Domain.Repositories;

namespace Domain.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ICostumerRepository costumerRepository;
        public CartItemService(ICartItemRepository cartItemRepository, ICostumerRepository costumerRepository)
        {
            _cartItemRepository = cartItemRepository;
            this.costumerRepository = costumerRepository;
        }

        public async Task<IEnumerable<CartItemResponseDto>> GetAllCartItemsByCostumerIdAsync(Guid costumerIdAsync, CancellationToken cancellationToken)
        {
            return await _cartItemRepository.GetAllByCostumerIdAsync(costumerIdAsync, cancellationToken);
        }

        public async Task CreateCartItemAsync(Guid userId, CreateCartItemDto request, CancellationToken cancellationToken)
        {
            
            //TODO: Implement domain validations and throw a domain exception
            //TODO: Add data annotations to the CreateCartItemDto instead of doing the validation here
            if (request is null)
                throw new ArgumentNullException(nameof(request), "request cannot be null");

            if (string.IsNullOrWhiteSpace(request.ProductName))
                throw new ArgumentException("ProductName cannot be null, empty or whitespace", nameof(request));

            if (request.ProductPrice <= 0)
                throw new ArgumentException("ProductPrice must be greater than zero", nameof(request));

            Costumer costumer = await costumerRepository.GetByIdAsync(userId, cancellationToken);

            if(costumer is null)
                throw new ArgumentException("Costumer not found", nameof(userId));

            var existingItem = await _cartItemRepository.GetByCostumerIdAndProductNameAsync(userId, request.ProductName, cancellationToken);

            if (existingItem is null)
            {
                var cartItem = new CartItem(userId, request.ProductName, request.ProductPrice);
                await _cartItemRepository.AddAsync(cartItem, cancellationToken);
            }
            else
            {
                existingItem.UpdateQuantity(existingItem.Quantity + 1);
                await _cartItemRepository.UpdateAsync(existingItem, cancellationToken);
            }

            await _cartItemRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateCartItemQuantityAsync(Guid cartItemId, int newQuantity, CancellationToken cancellationToken)
        {
            if (newQuantity < 0)
                throw new ArgumentException("Quantity should'nt be negative");

            var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId, cancellationToken);

            if (cartItem is null)
                throw new ArgumentException("Cart item not found");

            if (newQuantity == 0)
            {
                await _cartItemRepository.DeleteAsync(cartItem, cancellationToken);
            }
            else
            {
                cartItem.UpdateQuantity(newQuantity);
                await _cartItemRepository.UpdateAsync(cartItem, cancellationToken);
            }

            await _cartItemRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
