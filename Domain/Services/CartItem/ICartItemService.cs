﻿using Domain.Dtos;

namespace Domain.Services
{
    public interface ICartItemService
    {
        public Task CreateCartItemAsync(Guid userId, CreateCartItemDto request, CancellationToken cancellationToken);
        public Task UpdateCartItemQuantityAsync(Guid cartItemId, int newQuantity, CancellationToken cancellationToken);
        public Task<IEnumerable<CartItemResponseDto>> GetAllCartItemsByCostumerIdAsync(Guid costumerIdAsync, CancellationToken cancellationToken);
    }
}
