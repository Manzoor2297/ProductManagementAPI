using ProductManagementAPI.Models;

namespace ProductManagementAPI.Repositories
{
    public interface IProductRepository
    {
        Task<Product> AddProductAsync(Product product);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int productId);
        Task<Product?> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int productId);
        Task<int> GenerateProductIdAsync();
    }
}
