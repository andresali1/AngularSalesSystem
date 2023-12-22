namespace SalesSystem.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public int? CategoryId { get; set; }

        public string? CategoryDescription { get; set; }

        public int? Stock { get; set; }

        public string? Price { get; set; }

        public int? IsActive { get; set; }
    }
}
