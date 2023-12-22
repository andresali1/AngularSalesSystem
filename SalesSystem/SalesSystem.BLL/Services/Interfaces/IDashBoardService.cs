using SalesSystem.DTO;

namespace SalesSystem.BLL.Services.Interfaces
{
    public interface IDashBoardService
    {
        Task<DashBoardDTO> Summary();
    }
}
