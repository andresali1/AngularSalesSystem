namespace SalesSystem.DTO
{
    public class DashBoardDTO
    {
        public int SalesTotal { get; set; }

        public string? IncomeTotal { get; set; }

        public List<WeekSaleDTO> LastWeekSales { get; set; }
    }
}
