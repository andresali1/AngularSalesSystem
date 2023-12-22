using SalesSystem.DTO;

namespace SalesSystem.BLL.Services.Interfaces
{
    public interface ISaleService
    {
        Task<SaleDTO> Register(SaleDTO model);
        Task<List<SaleDTO>> History(string searchBy, string saleNumber, string beginDate, string endDate);
        Task<List<ReportDTO>> Report(string beginDate, string endDate);
    }
}
