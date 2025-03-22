using AutoFixture;
using Domain.Dtos;
using Domain.Repositories;
using Domain.Services;
using Domain.Entities;
using Moq;

namespace Tests.Services
{
    public class CostumerServiceTest : IClassFixture<CostumerServiceFixture>
    {
        private readonly CostumerServiceFixture _fixture;
        private readonly ICostumerService _sut;
        private readonly Mock<ICostumerRepository> _costumerRepositoryMock;
        private readonly IFixture _autoFixture;

        public CostumerServiceTest(CostumerServiceFixture fixture)
        {
            _fixture = fixture;
            _costumerRepositoryMock = fixture.CostumerRepositoryMock;
            _autoFixture = fixture.AutoFixture;
            _sut = new CostumerService(_costumerRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateOrUpdateCostumerAsync_WithNewCostumer_ShouldCreateCostumer()
        {
            // Arrange
            var request = _autoFixture.Create<CreateUpdateCostumerDto>();
            _costumerRepositoryMock.Setup(x => x.GetByEmailAsync(request.Email))
                .ReturnsAsync((Costumer)null);

            // Act
            await _sut.CreateOrUpdateCostumerAsync(request);

            // Assert
            _costumerRepositoryMock.Verify(x => x.AddAsync(
                It.Is<Costumer>(c =>
                    c.Name == request.Name &&
                    c.Email == request.Email)), Times.Once);
            _costumerRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateOrUpdateCostumerAsync_WithExistingCostumer_ShouldUpdateCostumer()
        {
            // Arrange
            var request = _autoFixture.Create<CreateUpdateCostumerDto>();
            var existingCostumer = _autoFixture.Build<Costumer>()
                .With(x => x.Email, request.Email)
                .Create();

            _costumerRepositoryMock.Setup(x => x.GetByEmailAsync(request.Email))
                .ReturnsAsync(existingCostumer);

            // Act
            await _sut.CreateOrUpdateCostumerAsync(request);

            // Assert
            _costumerRepositoryMock.Verify(x => x.UpdateAsync(
                It.Is<Costumer>(c =>
                    c.Id == existingCostumer.Id &&
                    c.Name == request.Name &&
                    c.Email == request.Email)), Times.Once);
            _costumerRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateOrUpdateCostumerAsync_WithNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            CreateUpdateCostumerDto request = null;

            // Act
            await _sut.CreateOrUpdateCostumerAsync(request);

            // Assert
            _costumerRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Costumer>()), Times.Never);
            _costumerRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Costumer>()), Times.Never);
            _costumerRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);
        }
    }
}
