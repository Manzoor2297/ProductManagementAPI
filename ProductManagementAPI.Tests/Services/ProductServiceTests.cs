using Microsoft.Extensions.Logging;
using Moq;
using ProductManagementAPI.Models;
using ProductManagementAPI.Repositories;
using ProductManagementAPI.Services;

namespace ProductManagementAPI.Tests.Services
{
    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _mockProductRepository;
        private Mock<ILogger<ProductService>> _mockLogger;
        private ProductService _productService;

        [SetUp]
        public void Setup()
        {
            this._mockLogger = new Mock<ILogger<ProductService>>();
            this._mockProductRepository = new Mock<IProductRepository>();
            _productService = new ProductService(_mockProductRepository.Object, _mockLogger.Object);
        }

        [Test]
        public async Task CreateProductAsync_Should_Call_AddProductAsync()
        {
            // Arrange
            var product = new Product { Name = "Test Product", StockAvailable = 10, Description = "Test Description", Category = "Category", Price = 500, ProductId = 1 };

            _mockProductRepository.Setup(repo => repo.AddProductAsync(It.IsAny<Product>())).ReturnsAsync(product);

            // Act
            await _productService.CreateProductAsync(product);

            // Assert
            _mockProductRepository.Verify(repo => repo.AddProductAsync(product), Times.Once);
        }

        [Test]
        public async Task GetProductByIdAsync_Should_Return_Product()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Test Product", StockAvailable = 10 };
            _mockProductRepository.Setup(repo => repo.GetProductByIdAsync(1)).ReturnsAsync(product);

            // Act
            var result = await _productService.GetProductByIdAsync(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ProductId, Is.EqualTo(product.ProductId));
        }
    }
}
