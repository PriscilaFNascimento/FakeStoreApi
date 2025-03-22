using AutoFixture;
using Domain.Repositories;
using Moq;

namespace Tests.Services
{
    public class CostumerServiceFixture
    {
        public Mock<ICostumerRepository> CostumerRepositoryMock { get; }
        public IFixture AutoFixture { get; }

        public CostumerServiceFixture()
        {
            CostumerRepositoryMock = new Mock<ICostumerRepository>();
            AutoFixture = new Fixture();
        }
    }
}
