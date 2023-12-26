using Microsoft.AspNetCore.Mvc;
using SalesSystem.API.Utility;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DTO;

namespace SalesSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserService _appUserService;

        public AppUserController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        /// <summary>
        /// Métod to get the list of users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var res = new Response<List<AppUserDTO>>();

            try
            {
                res.status = true;
                res.value = await _appUserService.List();
            }
            catch (Exception ex)
            {
                res.status = false;
                res.msg = ex.Message;
            }

            return Ok(res);
        }

        /// <summary>
        /// Method to login an user
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO credentials)
        {
            var res = new Response<SessionDTO>();

            try
            {
                res.status = true;
                res.value = await _appUserService.CredentialsValidation(credentials.Email, credentials.Pass);
            }
            catch (Exception ex)
            {
                res.status = false;
                res.msg = ex.Message;
            }

            return Ok(res);
        }

        /// <summary>
        /// Method to save an user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save([FromBody] AppUserDTO user)
        {
            var res = new Response<AppUserDTO>();

            try
            {
                res.status = true;
                res.value = await _appUserService.Create(user);
            }
            catch (Exception ex)
            {
                res.status = false;
                res.msg = ex.Message;
            }

            return Ok(res);
        }

        /// <summary>
        /// Method to edit an user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit([FromBody] AppUserDTO user)
        {
            var res = new Response<bool>();

            try
            {
                res.status = true;
                res.value = await _appUserService.Edit(user);
            }
            catch (Exception ex)
            {
                res.status = false;
                res.msg = ex.Message;
            }

            return Ok(res);
        }

        /// <summary>
        /// Method to delete an user
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
                res.value = await _appUserService.Delete(id);
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
