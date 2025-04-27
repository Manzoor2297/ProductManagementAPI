namespace ProductManagementAPI.Models
{
    public class Product
    {
        public int ProductId { get; set; } // Auto-generated 6-digit number
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockAvailable { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}
