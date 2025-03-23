using AutoFixture;
using Domain.Repositories;
using Moq;

namespace Tests.Services
{
    public class OrderServiceFixture
    {
        public Mock<IOrderRepository> OrderRepositoryMock { get; }
        public Mock<ICartItemRepository> CartItemRepositoryMock { get; }
        public Mock<ICostumerRepository> CostumerRepositoryMock { get; }
        public Mock<IOrderProductRepository> OrderProductRepositoryMock { get; }
        public IFixture AutoFixture { get; }

        public OrderServiceFixture()
        {
            OrderRepositoryMock = new Mock<IOrderRepository>();
            CartItemRepositoryMock = new Mock<ICartItemRepository>();
            CostumerRepositoryMock = new Mock<ICostumerRepository>();
            OrderProductRepositoryMock = new Mock<IOrderProductRepository>();
            AutoFixture = new Fixture();
        }

        public void ResetMocks()
        {
            OrderRepositoryMock.Reset();
            CartItemRepositoryMock.Reset();
            CostumerRepositoryMock.Reset();
        }
    }
}
