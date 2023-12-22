using SalesSystem.DTO;

namespace SalesSystem.BLL.Services.Interfaces
{
    public interface IAppUserService
    {
        Task<List<AppUserDTO>> List();
        Task<SessionDTO> CredentialsValidation(string email, string password);
        Task<AppUserDTO> Create(AppUserDTO model);
        Task<bool> Edit(AppUserDTO model);
        Task<bool> Delete(int id);
    }
}
