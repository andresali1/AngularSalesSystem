using SalesSystem.Model;

namespace SalesSystem.DAL.Repositories.Interfaces
{
    public interface ISaleRepository : IGenericRepository<Sale>
    {
        Task<Sale> Register(Sale model);
    }
}
