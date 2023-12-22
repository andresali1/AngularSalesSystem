namespace SalesSystem.DTO
{
    public class SaleDetailDTO
    {
        public int? ProductId { get; set; }

        public string? ProductDescription { get; set; }

        public int? Amount { get; set; }

        public string? PriceText { get; set; }

        public string? TotalText { get; set; }
    }
}
