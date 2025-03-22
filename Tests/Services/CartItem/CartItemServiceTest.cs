using AutoFixture;
using Domain.Dtos;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using FluentAssertions;
using Moq;

namespace Tests.Services
{
    public class CartItemServiceTest : IClassFixture<CartItemServiceFixture>
    {
        private readonly CartItemServiceFixture _fixture;
        private readonly ICartItemService _sut;
        private readonly Mock<ICartItemRepository> _cartItemRepositoryMock;
        private readonly IFixture _autoFixture;

        public CartItemServiceTest(CartItemServiceFixture fixture)
        {
            _fixture = fixture;
            _cartItemRepositoryMock = fixture.CartItemRepositoryMock;
            _autoFixture = fixture.AutoFixture;
            _sut = new CartItemService(_cartItemRepositoryMock.Object);
            _fixture.ResetMocks();
        }

        [Fact]
        public async Task CreateCartItemAsync_WithValidRequest_ShouldCreateCartItem()
        {
            // Arrange
            var userId = _autoFixture.Create<Guid>();
            var request = _autoFixture.Create<CreateCartItemDto>();
            _cartItemRepositoryMock.Setup(x => x.GetByCostumerIdAndProductNameAsync(userId, request.ProductName, CancellationToken.None))
                .ReturnsAsync((CartItem)null);

            // Act
            await _sut.CreateCartItemAsync(userId, request, CancellationToken.None);

            // Assert
            _cartItemRepositoryMock.Verify(x => x.AddAsync(
                It.Is<CartItem>(ci =>
                    ci.CostumerId == userId &&
                    ci.ProductName == request.ProductName &&
                    ci.ProductPrice == request.ProductPrice &&
                    ci.Quantity == 1), CancellationToken.None), Times.Once);
            _cartItemRepositoryMock.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task CreateCartItemAsync_WithExistingItem_ShouldUpdateQuantity()
        {
            // Arrange
            var userId = _autoFixture.Create<Guid>();
            var request = _autoFixture.Create<CreateCartItemDto>();

            var existingItem = _autoFixture.Build<CartItem>()
                .With(x => x.CostumerId, userId)
                .With(x => x.ProductName, request.ProductName)
                .Create();

            int oldQuantity = existingItem.Quantity;

            _cartItemRepositoryMock.Setup(x => x.GetByCostumerIdAndProductNameAsync(userId, request.ProductName, CancellationToken.None))
                .ReturnsAsync(existingItem);

            // Act
            await _sut.CreateCartItemAsync(userId, request, CancellationToken.None);

            // Assert
            _cartItemRepositoryMock.Verify(x => x.UpdateAsync(
                It.Is<CartItem>(ci =>
                    ci.CostumerId == userId &&
                    ci.ProductName == request.ProductName &&
                    ci.Quantity == oldQuantity + 1), CancellationToken.None), Times.Once);
            _cartItemRepositoryMock.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task CreateCartItemAsync_WithNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var userId = _autoFixture.Create<Guid>();
            CreateCartItemDto request = null;

            // Act
            Func<Task> act = () => _sut.CreateCartItemAsync(userId, request, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>()
                .WithParameterName("request")
                .WithMessage("*request cannot be null*");
            _cartItemRepositoryMock.Verify(x => x.AddAsync(It.IsAny<CartItem>(), CancellationToken.None), Times.Never);
            _cartItemRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<CartItem>(), CancellationToken.None), Times.Never);
            _cartItemRepositoryMock.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Never);
        }

        //TODO: Add theory tests
    }
}