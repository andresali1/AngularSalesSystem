using SalesSystem.DTO;

namespace SalesSystem.BLL.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDTO>> List();
        Task<ProductDTO> Create(ProductDTO model);
        Task<bool> Edit(ProductDTO model);
        Task<bool> Delete(int id);
    }
}
