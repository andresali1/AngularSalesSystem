using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.API.Utility;
using SalesSystem.BLL.Services;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DTO;

namespace SalesSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        /// <summary>
        /// Method to register a SAle
        /// </summary>
        /// <param name="sale"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] SaleDTO sale)
        {
            var res = new Response<SaleDTO>();

            try
            {
                res.status = true;
                res.value = await _saleService.Register(sale);
            }
            catch (Exception ex)
            {
                res.status = false;
                res.msg = ex.Message;
            }

            return Ok(res);
        }

        /// <summary>
        /// Method to get the Sales history
        /// </summary>
        /// <param name="searchBy"></param>
        /// <param name="saleNumber"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("History")]
        public async Task<IActionResult> History(string searchBy, string? saleNumber, string? beginDate, string? endDate)
        {
            var res = new Response<List<SaleDTO>>();
            saleNumber = saleNumber ?? "";
            beginDate = beginDate ?? "";
            endDate = endDate ?? "";

            try
            {
                res.status = true;
                res.value = await _saleService.History(searchBy, saleNumber, beginDate, endDate);
            }
            catch (Exception ex)
            {
                res.status = false;
                res.msg = ex.Message;
            }

            return Ok(res);
        }

        /// <summary>
        /// MEthod to get the sales report
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Report")]
        public async Task<IActionResult> Report(string? beginDate, string? endDate)
        {
            var res = new Response<List<ReportDTO>>();

            try
            {
                res.status = true;
                res.value = await _saleService.Report(beginDate, endDate);
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
