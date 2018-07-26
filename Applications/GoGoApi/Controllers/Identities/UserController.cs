using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domains.Identity.Models;
using Domains.Identity.Services;
using Groove.AspNetCore.Mvc;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoGoApi.Controllers.Identities
{
    [Route("api/user")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // TODO: route should be ""
        [Route("")]
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult GetUsers([DataSourceRequest]DataSourceRequest request)
        {
            var result = _userService.GetUsersAsync(request);
            return Ok(result);
        }

        // TODO: route should be ""
        [Route("")]
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateUser([FromBody]UserCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = await _userService.CreateUserAsync(model, GetCurrentIdentity<long>());

            return OkValueObject(userId);
        }

        // TODO: route shoule be "{id}"
        [Route("{id}")]
        [HttpPut]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateUser(long id, [FromBody]UserUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = await _userService.UpdateUserAsync(id, model, GetCurrentIdentity<long>());

            return OkValueObject(userId);
        }
        // TODO: route should be "{id}/editview" or consider to remove this API & use GetUserDetail instead
        // Get the value of user need to update
        //[Route("editview")]
        //[HttpGet]
        //[Authorize(Roles = "Administrator")]
        //public async Task<IActionResult> ViewUserUpdate(long id)
        //{
        //    return Ok(await _userService.GetUserUpdateAsync(id));
        //}


        // TODO: route should be {id}
        [Route("{id}")]
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetUserDetail(long? id)
        {
            return Ok(await _userService.GetUserDetailAsync(id));
        }
    }
}