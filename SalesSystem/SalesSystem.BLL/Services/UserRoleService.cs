using AutoMapper;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DAL.Repositories.Interfaces;
using SalesSystem.DTO;
using SalesSystem.Model;

namespace SalesSystem.BLL.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IGenericRepository<UserRole> _userRoleRepository;
        private readonly IMapper _mapper;

        public UserRoleService(IGenericRepository<UserRole> userRoleRepository, IMapper mapper)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Method to get all the user roles
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserRoleDTO>> List()
        {
            try
            {
                var userRolesList = await _userRoleRepository.Consult();
                return _mapper.Map<List<UserRoleDTO>>(userRolesList.ToList());
            }
            catch
            {
                throw;
            }
        }
    }
}
