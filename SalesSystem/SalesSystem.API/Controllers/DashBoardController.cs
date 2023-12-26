using Microsoft.AspNetCore.Mvc;
using SalesSystem.API.Utility;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DTO;

namespace SalesSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashBoardService _dashboardService;

        public DashBoardController(IDashBoardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        /// <summary>
        /// Method to get the dashboard information
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Summary")]
        public async Task<IActionResult> Summary()
        {
            var res = new Response<DashBoardDTO>();

            try
            {
                res.status = true;
                res.value = await _dashboardService.Summary();
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
