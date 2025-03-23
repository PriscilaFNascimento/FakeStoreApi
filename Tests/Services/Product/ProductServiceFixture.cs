using AutoFixture;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests.Services.Product
{
    public class ProductServiceFixture
    {
        public Mock<IHttpClientFactory> HttpClientFactoryMock { get; }
        public Mock<IConfiguration> ConfigurationMock { get; }
        public Mock<HttpMessageHandler> HttpMessageHandlerMock { get; }
        public IFixture AutoFixture { get; }

        public ProductServiceFixture()
        {
            HttpClientFactoryMock = new Mock<IHttpClientFactory>();
            ConfigurationMock = new Mock<IConfiguration>();
            HttpMessageHandlerMock = new Mock<HttpMessageHandler>();
            AutoFixture = new Fixture();

            ConfigurationMock.Setup(x => x["FakeStoreExternalApi:ProductsEndpoint"])
                .Returns("https://fakestoreapi.com/products/");
        }

        public void ResetMocks()
        {
            HttpClientFactoryMock.Reset();
            HttpMessageHandlerMock.Reset();
        }
    }
} 