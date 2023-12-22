using SalesSystem.DTO;

namespace SalesSystem.BLL.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> List();
    }
}
