using Microsoft.AspNetCore.Mvc;
using SalesSystem.API.Utility;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DTO;

namespace SalesSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Method to get the list of products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var res = new Response<List<ProductDTO>>();

            try
            {
                res.status = true;
                res.value = await _productService.List();
            }
            catch (Exception ex)
            {
                res.status = false;
                res.msg = ex.Message;
            }

            return Ok(res);
        }

        /// <summary>
        /// Methdo to create a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save([FromBody] ProductDTO product)
        {
            var res = new Response<ProductDTO>();

            try
            {
                res.status = true;
                res.value = await _productService.Create(product);
            }
            catch (Exception ex)
            {
                res.status = false;
                res.msg = ex.Message;
            }

            return Ok(res);
        }

        /// <summary>
        /// Method to edit a Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit([FromBody] ProductDTO product)
        {
            var res = new Response<bool>();

            try
            {
                res.status = true;
                res.value = await _productService.Edit(product);
            }
            catch (Exception ex)
            {
                res.status = false;
                res.msg = ex.Message;
            }

            return Ok(res);
        }

        /// <summary>
        /// Method to delete a Product by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = new Response<bool>();

            try
            {
                res.status = true;
                res.value = await _productService.Delete(id);
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
