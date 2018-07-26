using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains.Identity.Models;
using Domains.Identity.Services;
using Groove.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoGoApi.Controllers.Identities
{
    [Route("api/user")]
    public class UserProfileController : BaseController
    {
        private readonly IUserService _userService;
        public UserProfileController(IUserService userService)
        {
            _userService = userService;
        }

        // TODO: Move to profile controller
        // TODO: Remove Id, Id should be get from access token
        // TODO: Rename function to GetMyUserProfile
        // TODO: Route shoule be "myprofile"
        [Route("myprofile")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMyUserProfile()
        {
            //var result = await _userService.GetUserProfileAsync(GetCurrentUserId<long>());
            return Ok(await _userService.GetUserProfileAsync(GetCurrentUserId<long>()));
        }

        // TODO: Move to profile controller 
        // TODO: Remove Id, Id should be get from access token
        // TODO: Rename function to UpdateMyUserProfile
        // TODO: Route should be ""
        [Route("")]
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateMyUserProfile([FromBody]UserProfileUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = await _userService.UpdateUserProfileAsync(GetCurrentUserId<long>(), model, GetCurrentIdentity<long>());

            return OkValueObject(userId);
        }
    }
}