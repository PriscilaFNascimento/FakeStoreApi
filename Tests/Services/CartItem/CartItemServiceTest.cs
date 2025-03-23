using AutoFixture;
using Domain.Dtos;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using FluentAssertions;
using Moq;
using Moq.Language.Flow;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Services
{
    public class CartItemServiceTest : IClassFixture<CartItemServiceFixture>
    {
        private readonly CartItemServiceFixture _fixture;
        private readonly ICartItemService _sut;
        private readonly Mock<ICartItemRepository> _cartItemRepositoryMock;
        private readonly Mock<ICostumerRepository> _costumerRepositoryMock;
        private readonly IFixture _autoFixture;

        public CartItemServiceTest(CartItemServiceFixture fixture)
        {
            _fixture = fixture;
            _cartItemRepositoryMock = fixture.CartItemRepositoryMock;
            _costumerRepositoryMock = fixture.CostumerRepositoryMock;
            _autoFixture = fixture.AutoFixture;
            _sut = new CartItemService(_cartItemRepositoryMock.Object, _costumerRepositoryMock.Object);
            _fixture.ResetMocks();
        }

        [Fact]
        public async Task CreateCartItemAsync_WithValidRequest_ShouldCreateCartItem()
        {
            // Arrange
            var costumer = _autoFixture.Create<Costumer>();
            var request = _autoFixture.Create<CreateCartItemDto>();

            _costumerRepositoryMock.Setup(x => x.GetByIdAsync(costumer.Id, CancellationToken.None))
                .ReturnsAsync(costumer);
            _cartItemRepositoryMock.Setup(x => x.GetByCostumerIdAndProductNameAsync(costumer.Id, request.ProductName, CancellationToken.None))
                .ReturnsAsync((CartItem)null);

            // Act
            await _sut.CreateCartItemAsync(costumer.Id, request, CancellationToken.None);

            // Assert
            _cartItemRepositoryMock.Verify(x => x.AddAsync(
                It.Is<CartItem>(ci =>
                    ci.CostumerId == costumer.Id &&
                    ci.ProductName == request.ProductName &&
                    ci.ProductPrice == request.ProductPrice &&
                    ci.Quantity == 1), CancellationToken.None), Times.Once);
            _cartItemRepositoryMock.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task CreateCartItemAsync_WithExistingItem_ShouldUpdateQuantity()
        {
            // Arrange
            var costumer = _autoFixture.Create<Costumer>();
            var request = _autoFixture.Create<CreateCartItemDto>();

            var existingItem = _autoFixture.Build<CartItem>()
                .With(x => x.CostumerId, costumer.Id)
                .With(x => x.ProductName, request.ProductName)
                .Create();

            int oldQuantity = existingItem.Quantity;

            _cartItemRepositoryMock.Setup(x => x.GetByCostumerIdAndProductNameAsync(costumer.Id, request.ProductName, CancellationToken.None))
                .ReturnsAsync(existingItem);
            _costumerRepositoryMock.Setup(x => x.GetByIdAsync(costumer.Id, CancellationToken.None))
                .ReturnsAsync(costumer);

            // Act
            await _sut.CreateCartItemAsync(costumer.Id, request, CancellationToken.None);

            // Assert
            _cartItemRepositoryMock.Verify(x => x.UpdateAsync(
                It.Is<CartItem>(ci =>
                    ci.CostumerId == costumer.Id &&
                    ci.ProductName == request.ProductName &&
                    ci.Quantity == oldQuantity + 1), CancellationToken.None), Times.Once);
            _cartItemRepositoryMock.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task CreateCartItemAsync_WithNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var costumer = _autoFixture.Create<Costumer>();
            _costumerRepositoryMock.Setup(x => x.GetByIdAsync(costumer.Id, CancellationToken.None))
                .ReturnsAsync(costumer);
            CreateCartItemDto request = null;

            // Act
            Func<Task> act = () => _sut.CreateCartItemAsync(costumer.Id, request, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>()
                .WithParameterName("request")
                .WithMessage("*request cannot be null*");
            _cartItemRepositoryMock.Verify(x => x.AddAsync(It.IsAny<CartItem>(), CancellationToken.None), Times.Never);
            _cartItemRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<CartItem>(), CancellationToken.None), Times.Never);
            _cartItemRepositoryMock.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task UpdateCartItemQuantityAsync_WithValidData_ShouldUpdateQuantity()
        {
            // Arrange
            var cartItemId = Guid.NewGuid();
            var cartItem = _autoFixture.Build<CartItem>()
                .With(x => x.Id, cartItemId)
                .Create();
            int newQuantity = 5;

            _cartItemRepositoryMock.Setup(x => x.GetByIdAsync(cartItemId, CancellationToken.None))
                .ReturnsAsync(cartItem);

            // Act
            await _sut.UpdateCartItemQuantityAsync(cartItemId, newQuantity, CancellationToken.None);

            // Assert
            _cartItemRepositoryMock.Verify(x => x.UpdateAsync(
                It.Is<CartItem>(ci =>
                    ci.Id == cartItemId &&
                    ci.Quantity == newQuantity), CancellationToken.None), Times.Once);
            _cartItemRepositoryMock.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task UpdateCartItemQuantityAsync_WithZeroQuantity_ShouldDeleteCartItem()
        {
            // Arrange
            var cartItemId = Guid.NewGuid();
            var cartItem = _autoFixture.Build<CartItem>()
                .With(x => x.Id, cartItemId)
                .Create();
            int newQuantity = 0;

            _cartItemRepositoryMock.Setup(x => x.GetByIdAsync(cartItemId, CancellationToken.None))
                .ReturnsAsync(cartItem);

            // Act
            await _sut.UpdateCartItemQuantityAsync(cartItemId, newQuantity, CancellationToken.None);

            // Assert
            _cartItemRepositoryMock.Verify(x => x.DeleteAsync(cartItem, CancellationToken.None), Times.Once);
            _cartItemRepositoryMock.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
            _cartItemRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<CartItem>(), CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task UpdateCartItemQuantityAsync_WithInvalidCartItemId_ShouldThrowArgumentException()
        {
            // Arrange
            var cartItemId = Guid.NewGuid();
            int newQuantity = 5;

            _cartItemRepositoryMock.Setup(x => x.GetByIdAsync(cartItemId, CancellationToken.None))
                .ReturnsAsync((CartItem)null);

            // Act
            Func<Task> act = () => _sut.UpdateCartItemQuantityAsync(cartItemId, newQuantity, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Cart item not found");
            _cartItemRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<CartItem>(), CancellationToken.None), Times.Never);
            _cartItemRepositoryMock.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Never);
        }

        [Theory]
        [InlineData(-1)]
        public async Task UpdateCartItemQuantityAsync_WithInvalidQuantity_ShouldThrowArgumentException(int invalidQuantity)
        {
            // Arrange
            var cartItemId = Guid.NewGuid();
            var cartItem = _autoFixture.Build<CartItem>()
                .With(x => x.Id, cartItemId)
                .Create();

            _cartItemRepositoryMock.Setup(x => x.GetByIdAsync(cartItemId, CancellationToken.None))
                .ReturnsAsync(cartItem);

            // Act
            Func<Task> act = () => _sut.UpdateCartItemQuantityAsync(cartItemId, invalidQuantity, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Quantity should'nt be negative");
            _cartItemRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<CartItem>(), CancellationToken.None), Times.Never);
            _cartItemRepositoryMock.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task GetAllCartItemsByCostumerIdAsync_WithValidCostumerId_ShouldReturnCartItems()
        {
            // Arrange
            var costumerId = Guid.NewGuid();
            var cartItems = _autoFixture.CreateMany<CartItem>(10).ToList();
            var expectedResponse = cartItems.Select(ci => new CartItemResponseDto 
            { 
                Id = ci.Id,
                ProductName = ci.ProductName,
                ProductPrice = ci.ProductPrice,
                Quantity = ci.Quantity
            }).ToList();

            _cartItemRepositoryMock.Setup(x => x.GetAllByCostumerIdAsync(costumerId, CancellationToken.None))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _sut.GetAllCartItemsByCostumerIdAsync(costumerId, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResponse);
            _cartItemRepositoryMock.Verify(x => x.GetAllByCostumerIdAsync(costumerId, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task GetAllCartItemsByCostumerIdAsync_WithNonExistentCostumerId_ShouldReturnEmptyCollection()
        {
            // Arrange
            var costumerId = Guid.NewGuid();
            _cartItemRepositoryMock.Setup(x => x.GetAllByCostumerIdAsync(costumerId, CancellationToken.None))
                .ReturnsAsync(new List<CartItemResponseDto>());

            // Act
            var result = await _sut.GetAllCartItemsByCostumerIdAsync(costumerId, CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
            _cartItemRepositoryMock.Verify(x => x.GetAllByCostumerIdAsync(costumerId, CancellationToken.None), Times.Once);
        }

        //TODO: Add theory tests
    }
}