using Microsoft.AspNetCore.Mvc;
using SalesSystem.API.Utility;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DTO;

namespace SalesSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Method to get the Categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var res = new Response<List<CategoryDTO>>();

            try
            {
                res.status = true;
                res.value = await _categoryService.List();
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
