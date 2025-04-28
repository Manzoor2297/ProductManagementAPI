using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Data;
using ProductManagementAPI.Models;
using ProductManagementAPI.Services;

namespace ProductManagementAPI.Repositories
{
    /// <summary>
    /// Product repository to operate with Database.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        /// <summary>
        /// Database Context field.
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Gets or Sets logger property.
        /// </summary>
        private readonly ILogger<ProductRepository> Logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="context">ApplicationDbContext.</param>
        public ProductRepository(ApplicationDbContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            Logger = logger;
        }

        /// <summary>
        /// Adds product.
        /// </summary>
        /// <param name="product">Product object.</param>
        /// <returns>Added product.</returns>
        public async Task<Product> AddProductAsync(Product product)
        {
            try
            {
                Logger.LogDebug("ProductRepository.GenerateProductIdAsync started");
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                Logger.LogDebug("ProductRepository.GenerateProductIdAsync ended");
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the product.", ex);
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
                Logger.LogDebug("ProductRepository.GetAllProductsAsync started");
                return await _context.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all products.", ex);
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
                Logger.LogDebug("ProductRepository.GetProductByIdAsync started");
                return await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching product by Id.", ex);
            }
        }

        /// <summary>
        /// Updates product by Id.
        /// </summary>
        /// <param name="product">Product.</param>
        /// <returns>Updated product.</returns>
        public async Task<Product?> UpdateProductAsync(Product product)
        {
            try
            {
                Logger.LogDebug("ProductRepository.UpdateProductAsync started");
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                Logger.LogDebug("ProductRepository.UpdateProductAsync ended");
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating product by Id.", ex);
            }
        }

        /// <summary>
        /// Deletes product by Id.
        /// </summary>
        /// <param name="productId">Product Id.</param>
        /// <returns>True or False.</returns>
        public async Task<bool> DeleteProductAsync(int productId)
        {
            Logger.LogDebug("ProductRepository.DeleteProductAsync started");
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product == null)
            {
                Logger.LogError("ProductRepository.DeleteProductAsync product with the Id not found");
                return false;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            Logger.LogDebug("ProductRepository.DeleteProductAsync ended");
            return true;
        }
    }
}
