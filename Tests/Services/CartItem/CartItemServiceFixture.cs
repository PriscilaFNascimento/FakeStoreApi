using AutoFixture;
using Domain.Repositories;
using Moq;

namespace Tests.Services
{
    public class CartItemServiceFixture
    {
        public Mock<ICartItemRepository> CartItemRepositoryMock { get; }
        public IFixture AutoFixture { get; }

        public CartItemServiceFixture()
        {
            CartItemRepositoryMock = new Mock<ICartItemRepository>();
            AutoFixture = new Fixture();
        }

        public void ResetMocks()
        {
            CartItemRepositoryMock.Reset();
        }
    }
}
