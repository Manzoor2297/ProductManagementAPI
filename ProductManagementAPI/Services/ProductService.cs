using ProductManagementAPI.Controllers;
using ProductManagementAPI.Models;
using ProductManagementAPI.Repositories;

namespace ProductManagementAPI.Services
{
    /// <summary>
    /// Service layer to handle any business logic on products.
    /// </summary>
    public class ProductService : IProductService
    {
        /// <summary>
        /// IProductRepository field.
        /// </summary>
        private readonly IProductRepository _repository;

        /// <summary>
        /// Gets or Sets logger property.
        /// </summary>
        private readonly ILogger<ProductService> Logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="repository">IProductRepository.</param>
        public ProductService(IProductRepository repository, ILogger<ProductService> logger)
        {
            _repository = repository;
            Logger = logger;
        }

        /// <summary>
        /// Creates product.
        /// </summary>
        /// <param name="product">Product object.</param>
        /// <returns>Added product.</returns>
        public async Task<Product> CreateProductAsync(Product product)
        {
            try
            {
                Logger.LogDebug("ProductService.CreateProductAsync started");
                return await _repository.AddProductAsync(product);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating product.", ex);
            }
        }

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>List of products.</returns>
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {
                Logger.LogDebug("ProductService.GetAllProductsAsync started");
                return await _repository.GetAllProductsAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting all products.", ex);
            }
        }

        /// <summary>
        /// Gets product by Id.
        /// </summary>
        /// <param name="productId">Product id.</param>
        /// <returns>Product by Id.</returns>
        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            try
            {
                Logger.LogDebug("ProductService.GetProductByIdAsync started");
                return await _repository.GetProductByIdAsync(productId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting product by id.", ex);
            }
        }

        /// <summary>
        /// Updates product by Id.
        /// </summary>
        /// <param name="productId">Product id.</param>
        /// <param name="updatedProduct">Updated product.</param>
        /// <returns>Updated product.</returns>
        public async Task<Product?> UpdateProductAsync(int productId, Product updatedProduct)
        {
            try
            {
                Logger.LogDebug("ProductService.UpdateProductAsync started");
                var existingProduct = await _repository.GetProductByIdAsync(productId);
                if (existingProduct == null)
                {
                    Logger.LogError("ProductService.UpdateProductAsync product with the Id not found");
                    return null;
                }

                existingProduct.Name = updatedProduct.Name;
                existingProduct.Description = updatedProduct.Description;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.StockAvailable = updatedProduct.StockAvailable;
                existingProduct.Category = updatedProduct.Category;

                Logger.LogDebug("ProductService.UpdateProductAsync ended");
                return await _repository.UpdateProductAsync(existingProduct);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating product by id.", ex);
            }
        }

        /// <summary>
        /// Deletes product by Id.
        /// </summary>
        /// <param name="productId">Product Id.</param>
        /// <returns>True or False.</returns>
        public async Task<bool> DeleteProductAsync(int productId)
        {
            try
            {
                Logger.LogDebug("ProductService.DeleteProductAsync started");
                return await _repository.DeleteProductAsync(productId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting product by id.", ex);
            }
        }

        /// <summary>
        /// Decrement stock of a product by Id.
        /// </summary>
        /// <param name="productId">Product id.</param>
        /// <param name="quantity">Quantity to decrement the stock by.</param>
        /// <returns>Updated product details.</returns>
        public async Task<Product?> DecrementStockAsync(int productId, int quantity)
        {
            try
            {
                Logger.LogDebug("ProductService.DecrementStockAsync started");
                var product = await _repository.GetProductByIdAsync(productId);
                if (product == null || product.StockAvailable < quantity)
                {
                    Logger.LogError("ProductService.DecrementStockAsync product with the Id not found");
                    return null;
                }

                product.StockAvailable -= quantity;
                await _repository.UpdateProductAsync(product);
                Logger.LogDebug("ProductService.DecrementStockAsync ended");
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while decrementing product stock by id.", ex);
            }
        }

        /// <summary>
        /// Increment stock of a product by Id.
        /// </summary>
        /// <param name="productId">Product id.</param>
        /// <param name="quantity">Quantity to increment the stock by.</param>
        /// <returns>Updated product details.</returns>
        public async Task<Product?> AddToStockAsync(int productId, int quantity)
        {
            try
            {
                Logger.LogDebug("ProductService.AddToStockAsync started");
                var product = await _repository.GetProductByIdAsync(productId);
                if (product == null)
                {
                    Logger.LogError("ProductService.AddToStockAsync product with the Id not found");
                    return null;
                }
                product.StockAvailable += quantity;
                await _repository.UpdateProductAsync(product);
                Logger.LogDebug("ProductService.AddToStockAsync ended");
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while incrementing product stock by id.", ex);
            }
        }
    }
}
