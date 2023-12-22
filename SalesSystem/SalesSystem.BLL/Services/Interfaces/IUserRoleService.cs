using SalesSystem.DTO;

namespace SalesSystem.BLL.Services.Interfaces
{
    public interface IUserRoleService
    {
        Task<List<UserRoleDTO>> List();
    }
}
