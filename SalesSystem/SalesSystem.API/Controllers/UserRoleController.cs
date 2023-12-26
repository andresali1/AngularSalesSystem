using Microsoft.AspNetCore.Mvc;
using SalesSystem.API.Utility;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DTO;

namespace SalesSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private  readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        /// <summary>
        /// Métod to get the list of roles of users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var res = new Response<List<UserRoleDTO>>();

            try
            {
                res.status = true;
                res.value = await _userRoleService.List();
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
