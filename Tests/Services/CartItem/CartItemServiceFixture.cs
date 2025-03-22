using AutoFixture;
using Domain.Repositories;
using Moq;

namespace Tests.Services
{
    public class CartItemServiceFixture
    {
        public Mock<ICartItemRepository> CartItemRepositoryMock { get; }
        public Mock<ICostumerRepository> CostumerRepositoryMock { get; }
        public IFixture AutoFixture { get; }

        public CartItemServiceFixture()
        {
            CartItemRepositoryMock = new Mock<ICartItemRepository>();
            CostumerRepositoryMock = new Mock<ICostumerRepository>();
            AutoFixture = new Fixture();
        }

        public void ResetMocks()
        {
            CartItemRepositoryMock.Reset();
        }
    }
}
