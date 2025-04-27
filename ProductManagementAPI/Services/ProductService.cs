using ProductManagementAPI.Models;
using ProductManagementAPI.Repositories;

namespace ProductManagementAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            //product.ProductId = await _repository.GenerateProductIdAsync();
            return await _repository.AddProductAsync(product);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _repository.GetAllProductsAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await _repository.GetProductByIdAsync(productId);
        }

        public async Task<Product?> UpdateProductAsync(int productId, Product updatedProduct)
        {
            var existingProduct = await _repository.GetProductByIdAsync(productId);
            if (existingProduct == null)
                return null;

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.StockAvailable = updatedProduct.StockAvailable;
            existingProduct.Category = updatedProduct.Category;

            return await _repository.UpdateProductAsync(existingProduct);
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            return await _repository.DeleteProductAsync(productId);
        }

        public async Task<Product?> DecrementStockAsync(int productId, int quantity)
        {
            var product = await _repository.GetProductByIdAsync(productId);
            if (product == null || product.StockAvailable < quantity)
                return null;

            product.StockAvailable -= quantity;
            await _repository.UpdateProductAsync(product);
            return product;
        }

        public async Task<Product?> AddToStockAsync(int productId, int quantity)
        {
            var product = await _repository.GetProductByIdAsync(productId);
            if (product == null)
                return null;

            product.StockAvailable += quantity;
            await _repository.UpdateProductAsync(product);
            return product;
        }
    }
}
