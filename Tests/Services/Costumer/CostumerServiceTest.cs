using AutoFixture;
using Domain.Dtos;
using Domain.Repositories;
using Domain.Services;
using Domain.Entities;
using Moq;
using FluentAssertions;
using System.Xml.Linq;

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
            _fixture.ResetMocks(); // Reset mocks before each test
        }

        [Fact]
        public async Task CreateOrUpdateCostumerAsync_WithNewCostumer_ShouldCreateCostumer()
        {
            // Arrange
            var request = _autoFixture.Create<CreateUpdateCostumerDto>();
            var expectedCostumer = new Costumer(request.Name, request.Email);
            var expectedResponse = new CostumerResponseDto
            {
                Id = expectedCostumer.Id,
                Name = request.Name,
                Email = request.Email
            };

            _costumerRepositoryMock.Setup(x => x.GetByEmailAsync(request.Email))
                .ReturnsAsync((Costumer)null);

            _costumerRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Costumer>(), CancellationToken.None))
                .Returns(Task.FromResult(expectedCostumer));

            // Act
            var result = await _sut.CreateOrUpdateCostumerAsync(request, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResponse, options => options.Excluding(x => x.Id));
            _costumerRepositoryMock.Verify(x => x.AddAsync(
                It.Is<Costumer>(c =>
                    c.Name == request.Name &&
                    c.Email == request.Email), CancellationToken.None), Times.Once);
            _costumerRepositoryMock.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task CreateOrUpdateCostumerAsync_WithExistingCostumer_ShouldUpdateCostumer()
        {
            // Arrange
            var request = _autoFixture.Create<CreateUpdateCostumerDto>();
            var existingCostumer = new Costumer(_autoFixture.Create<string>(), request.Email);
            var expectedResponse = new CostumerResponseDto()
            {
                Id = existingCostumer.Id, 
                Name = request.Name, 
                Email = request.Email
            };

            _costumerRepositoryMock.Setup(x => x.GetByEmailAsync(request.Email))
                .ReturnsAsync(existingCostumer);

            // Act
            var result = await _sut.CreateOrUpdateCostumerAsync(request, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResponse);
            _costumerRepositoryMock.Verify(x => x.UpdateAsync(
                It.Is<Costumer>(c =>
                    c.Id == existingCostumer.Id &&
                    c.Name == request.Name &&
                    c.Email == request.Email), CancellationToken.None), Times.Once);
            _costumerRepositoryMock.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task CreateOrUpdateCostumerAsync_WithNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            CreateUpdateCostumerDto request = null;

            // Act
            Func<Task> act = () => _sut.CreateOrUpdateCostumerAsync(request, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>()
                .WithParameterName("request")
                .WithMessage("*value cannot be null*");
        }

        //TODO: Add theory tests
    }
}
