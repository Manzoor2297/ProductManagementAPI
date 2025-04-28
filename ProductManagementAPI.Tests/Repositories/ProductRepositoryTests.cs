using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using ProductManagementAPI.Data;
using ProductManagementAPI.Models;
using ProductManagementAPI.Repositories;

namespace ProductManagementAPI.Tests.Repositories
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        private Mock<ILogger<ProductRepository>> _mockLogger;
        private ApplicationDbContext _context;
        private ProductRepository _repository;

        [SetUp]
        public void Setup()
        {
            this._mockLogger = new Mock<ILogger<ProductRepository>>();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ProductDb_Test")
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new ProductRepository(_context, _mockLogger.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddProductAsync_Should_Add_Product()
        {
            // Arrange
            var product = new Product { Name = "Test Product", StockAvailable = 100, Description = "Test Description", Category = "Category", Price = 500, ProductId = 1 };

            // Act
            await _repository.AddProductAsync(product);
            var productInDb = await _context.Products.FirstOrDefaultAsync(p => p.Name == "Test Product");

            // Assert
            Assert.That(productInDb, Is.Not.Null);
            Assert.That(100, Is.EqualTo(productInDb.StockAvailable));
        }
    }
}
