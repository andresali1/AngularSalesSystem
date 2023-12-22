using SalesSystem.DTO;

namespace SalesSystem.BLL.Services.Interfaces
{
    public interface IMenuService
    {
        Task<List<MenuDTO>> List(int userId);
    }
}
