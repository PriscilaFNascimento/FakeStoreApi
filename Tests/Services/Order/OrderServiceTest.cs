using AutoFixture;
using Domain.Dtos;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Services
{
    public class OrderServiceTest : IClassFixture<OrderServiceFixture>
    {
        private readonly OrderServiceFixture _fixture;
        private readonly IOrderService _sut;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<ICartItemRepository> _cartItemRepositoryMock;
        private readonly Mock<ICostumerRepository> _costumerRepositoryMock;
        private readonly Mock<IOrderProductRepository> _orderProductRepositoryMock;
        private readonly IFixture _autoFixture;

        public OrderServiceTest(OrderServiceFixture fixture)
        {
            _fixture = fixture;
            _orderRepositoryMock = fixture.OrderRepositoryMock;
            _cartItemRepositoryMock = fixture.CartItemRepositoryMock;
            _costumerRepositoryMock = fixture.CostumerRepositoryMock;
            _orderProductRepositoryMock = fixture.OrderProductRepositoryMock;
            _autoFixture = fixture.AutoFixture;
            _sut = new OrderService(_orderRepositoryMock.Object, _costumerRepositoryMock.Object, _cartItemRepositoryMock.Object, _orderProductRepositoryMock.Object);
            _fixture.ResetMocks();
        }

        //[Fact]
        //public async Task GetAllOrdersByCostumerId_WithValidCostumerId_ShouldReturnOrders()
        //{
        //    // Arrange
        //    var costumerId = _autoFixture.Create<Guid>();
        //    var expectedOrders = _autoFixture.CreateMany<OrderResponseDto>().ToList();

        //    _orderRepositoryMock.Setup(x => x.GetAllByCostumerIdAsync(costumerId, It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(expectedOrders);

        //    // Act
        //    var result = await _sut.GetAllOrdersByCostumerId(costumerId);

        //    // Assert
        //    result.Should().BeEquivalentTo(expectedOrders);
        //    _orderRepositoryMock.Verify(x => x.GetAllByCostumerIdAsync(costumerId, It.IsAny<CancellationToken>()), Times.Once);
        //}

        //[Fact]
        //public async Task GetAllOrdersByCostumerId_WithNonExistentCostumerId_ShouldReturnEmptyCollection()
        //{
        //    // Arrange
        //    var costumerId = _autoFixture.Create<Guid>();
        //    var emptyList = new List<OrderResponseDto>();

        //    _orderRepositoryMock.Setup(x => x.GetAllByCostumerIdAsync(costumerId, It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(emptyList);

        //    // Act
        //    var result = await _sut.GetAllOrdersByCostumerId(costumerId);

        //    // Assert
        //    result.Should().BeEmpty();
        //    _orderRepositoryMock.Verify(x => x.GetAllByCostumerIdAsync(costumerId, It.IsAny<CancellationToken>()), Times.Once);
        //}

        [Fact]
        public async Task CreateOrderFromCostumerCartAsync_WithValidCostumerId_ShouldCreateOrder()
        {
            // Arrange
            var costumerId = _autoFixture.Create<Guid>();
            var costumer = _autoFixture.Create<Costumer>();
            var cartItems = _autoFixture.CreateMany<CartItemResponseDto>().ToList();

            _costumerRepositoryMock.Setup(x => x.GetByIdAsync(costumerId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(costumer);

            _cartItemRepositoryMock.Setup(x => x.GetAllByCostumerIdAsync(costumerId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(cartItems);

            // Act
            await _sut.CreateOrderFromCostumerCartAsync(costumerId);

            // Assert
            _orderRepositoryMock.Verify(x => x.AddAsync(
                It.Is<Order>(o =>
                    o.CostumerId == costumerId), It.IsAny<CancellationToken>()), Times.Once);
            _orderRepositoryMock.Verify(x => x.AddAsync(
                It.Is<Order>(o =>
                    o.CostumerId == costumerId), It.IsAny<CancellationToken>()), Times.Once);
            _orderProductRepositoryMock.Verify(x => x.AddRangeAsync(It.IsAny<IEnumerable<OrderProduct>>(), It.IsAny<CancellationToken>()), Times.Once);
            _orderRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _cartItemRepositoryMock.Verify(x => x.DeleteAllByCostumerId(costumerId), Times.Once);
        }

        [Fact]
        public async Task CreateOrderFromCostumerCartAsync_WithNonExistentCostumerId_ShouldThrowArgumentException()
        {
            // Arrange
            var costumerId = _autoFixture.Create<Guid>();

            _costumerRepositoryMock.Setup(x => x.GetByIdAsync(costumerId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Costumer)null);

            // Act
            Func<Task> act = () => _sut.CreateOrderFromCostumerCartAsync(costumerId);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("*Customer not found*");
            _orderRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Never);
            _orderRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task CreateOrderFromCostumerCartAsync_WithEmptyCart_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var costumerId = _autoFixture.Create<Guid>();
            var costumer = _autoFixture.Create<Costumer>();
            var emptyCart = new List<CartItemResponseDto>();

            _costumerRepositoryMock.Setup(x => x.GetByIdAsync(costumerId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(costumer);

            _cartItemRepositoryMock.Setup(x => x.GetAllByCostumerIdAsync(costumerId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(emptyCart);

            // Act
            Func<Task> act = () => _sut.CreateOrderFromCostumerCartAsync(costumerId);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("*Cart is empty*");
            _orderRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Never);
            _orderRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
