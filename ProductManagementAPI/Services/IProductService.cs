using ProductManagementAPI.Models;

namespace ProductManagementAPI.Services
{
    public interface IProductService
    {
        Task<Product> CreateProductAsync(Product product);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int productId);
        Task<Product?> UpdateProductAsync(int productId, Product product);
        Task<bool> DeleteProductAsync(int productId);
        Task<Product?> DecrementStockAsync(int productId, int quantity);
        Task<Product?> AddToStockAsync(int productId, int quantity);
    }

}
