using AutoMapper;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DAL.Repositories.Interfaces;
using SalesSystem.DTO;
using SalesSystem.Model;

namespace SalesSystem.BLL.Services
{
    public class MenuService : IMenuService
    {
        private readonly IGenericRepository<AppUser> _appUserRepository;
        private readonly IGenericRepository<RoleMenu> _roleMenuRepository;
        private readonly IGenericRepository<Menu> _menuRepository;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepository<AppUser> appUserRepository,
                           IGenericRepository<RoleMenu> roleMenuRepository,
                           IGenericRepository<Menu> menuRepository, IMapper mapper)
        {
            _appUserRepository = appUserRepository;
            _roleMenuRepository = roleMenuRepository;
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Method to get the menus by user role
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<MenuDTO>> List(int userId)
        {
            IQueryable<AppUser> tbUser = await _appUserRepository.Consult(u => u.UserId == userId);
            IQueryable<RoleMenu> tbRoleMenu = await _roleMenuRepository.Consult();
            IQueryable<Menu> tbMenu = await _menuRepository.Consult();

            try
            {
                IQueryable<Menu> tbResult =
                    (from u in tbUser
                     join rm in tbRoleMenu on u.Role.RoleId equals rm.RoleId
                     join m in tbMenu on rm.MenuId equals m.MenuId
                     select m).AsQueryable();

                var menusList = tbResult.ToList();
                return _mapper.Map<List<MenuDTO>>(menusList);
            }
            catch
            {
                throw;
            }
        }
    }
}
