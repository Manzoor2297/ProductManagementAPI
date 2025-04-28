using Microsoft.AspNetCore.Mvc;
using ProductManagementAPI.Models;
using ProductManagementAPI.Services;

namespace ProductManagementAPI.Controllers
{
    /// <summary>
    /// Controller to perform operations on Products.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        /// <summary>
        /// IProductService field.
        /// </summary>
        private readonly IProductService _service;

        /// <summary>
        /// Gets or Sets logger property.
        /// </summary>
        private readonly ILogger<ProductsController> Logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="productsService">IProductsService.</param>
        public ProductsController(IProductService productsService, ILogger<ProductsController> logger)
        {
            _service = productsService;
            Logger = logger;
        }

        /// <summary>
        /// Creates product object.
        /// </summary>
        /// <param name="product">Product.</param>
        /// <returns>Created Product.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            try
            {
                Logger.LogDebug("ProductsController.CreateProduct started");
                var createdProduct = await _service.CreateProductAsync(product);
                if (createdProduct == null)
                {
                    Logger.LogError("ProductsController.CreateProduct failed to create the product");
                    return BadRequest();
                }
                Logger.LogDebug("ProductsController.CreateProduct ended");
                return Ok(createdProduct);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating product.", ex);
            }
        }

        /// <summary>
        /// Gets list of products.
        /// </summary>
        /// <returns>List of Products.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                Logger.LogDebug("ProductsController.GetAllProducts started");
                var products = await _service.GetAllProductsAsync();
                if (products == null)
                {
                    Logger.LogError("ProductsController.GetAllProducts failed to get all the products");
                    return BadRequest();
                }
                Logger.LogDebug("ProductsController.GetAllProducts ended");
                return Ok(products);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting all the products.", ex);
            }
        }

        /// <summary>
        /// Gets product by Id.
        /// </summary>
        /// <param name="id">Product Id.</param>
        /// <returns>Returns Product object by Id.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                Logger.LogDebug("ProductsController.GetProductById started");
                var product = await _service.GetProductByIdAsync(id);
                if (product == null)
                {
                    Logger.LogError("ProductsController.GetProductById failed to get the product id");
                    return NotFound();
                }
                Logger.LogDebug("ProductsController.GetProductById ended");
                return Ok(product);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the product by id.", ex);
            }
        }

        /// <summary>
        /// Updates the product details based on Id.
        /// </summary>
        /// <param name="id">Product Id.</param>
        /// <param name="updatedProduct">Updated product details.</param>
        /// <returns>Updated product.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product updatedProduct)
        {
            try
            {
                Logger.LogDebug("ProductsController.UpdateProduct started");
                var product = await _service.UpdateProductAsync(id, updatedProduct);
                if (product == null)
                {
                    Logger.LogError("ProductsController.UpdateProduct failed to update the product");
                    return BadRequest();
                }
                Logger.LogDebug("ProductsController.UpdateProduct ended");
                return Ok(product);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the product by id.", ex);
            }
        }

        /// <summary>
        /// Deletes the product by Id.
        /// </summary>
        /// <param name="id">Product Id.</param>
        /// <returns>Ok response.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                Logger.LogDebug("ProductsController.DeleteProduct started");
                var deleted = await _service.DeleteProductAsync(id);
                if (!deleted)
                {
                    Logger.LogError("ProductsController.DeleteProduct failed to update the product");
                    return NotFound();
                }
                Logger.LogDebug("ProductsController.DeleteProduct ended");
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the product by id.", ex);
            }
        }

        /// <summary>
        /// Decrement the stock count of a product based on the Id.
        /// </summary>
        /// <param name="id">Product Id.</param>
        /// <param name="quantity">Quantity to decrement by.</param>
        /// <returns>Updated product details.</returns>
        [HttpPut("decrement-stock/{id}/{quantity}")]
        public async Task<IActionResult> DecrementStock(int id, int quantity)
        {
            try
            {
                Logger.LogDebug("ProductsController.DecrementStock started");
                var product = await _service.DecrementStockAsync(id, quantity);
                if (product == null)
                {
                    Logger.LogError("ProductsController.DecrementStock failed to decrement the product stock");
                    return BadRequest();
                }
                Logger.LogDebug("ProductsController.DecrementStock ended");
                return Ok(product);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while decrementing the product stock by id.", ex);
            }
        }

        /// <summary>
        /// Increments the stock count of a product based on the Id.
        /// </summary>
        /// <param name="id">Product Id.</param>
        /// <param name="quantity">Quantity to increment by.</param>
        /// <returns>Updated product details.</returns>
        [HttpPut("add-to-stock/{id}/{quantity}")]
        public async Task<IActionResult> AddToStock(int id, int quantity)
        {
            try
            {
                Logger.LogDebug("ProductsController.AddToStock started");
                var product = await _service.AddToStockAsync(id, quantity);
                if (product == null)
                {
                    Logger.LogError("ProductsController.AddToStock failed to increment the product stock");
                    return BadRequest();

                }
                Logger.LogDebug("ProductsController.AddToStock ended");
                return Ok(product);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while incrementing the product stock by id.", ex);
            }
        }
    }
}
