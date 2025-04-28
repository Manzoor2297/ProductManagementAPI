using ProductManagementAPI.Models;

namespace ProductManagementAPI.Repositories
{
    /// <summary>
    /// Interface for ProductRepository.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Adds product.
        /// </summary>
        Task<Product> AddProductAsync(Product product);

        /// <summary>
        /// Gets all products.
        /// </summary>
        Task<IEnumerable<Product>> GetAllProductsAsync();

        /// <summary>
        /// Gets product by Id.
        /// </summary>
        Task<Product?> GetProductByIdAsync(int productId);

        /// <summary>
        /// Updates product by Id.
        /// </summary>
        Task<Product?> UpdateProductAsync(Product product);

        /// <summary>
        /// Deletes product by Id.
        /// </summary>
        Task<bool> DeleteProductAsync(int productId);
    }
}
