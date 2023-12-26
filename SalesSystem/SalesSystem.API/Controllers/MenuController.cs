using Microsoft.AspNetCore.Mvc;
using SalesSystem.API.Utility;
using SalesSystem.BLL.Services;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DTO;

namespace SalesSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        /// <summary>
        /// Method to get the menus per user
        /// </summary>
        /// <param name="appUserId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List(int appUserId)
        {
            var res = new Response<List<MenuDTO>>();

            try
            {
                res.status = true;
                res.value = await _menuService.List(appUserId);
            }
            catch (Exception ex)
            {
                res.status = false;
                res.msg = ex.Message;
            }

            return Ok(res);
        }
    }
}
