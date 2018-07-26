using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains.Identity.Services;
using GoGoApi.Models.Identities;
using Groove.AspNetCore.Mvc;
using Groove.AspNetCore.Mvc.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GoGoApi.Controllers.Identities
{
    [Route("api/authentication")]
    public class AuthenticationController : BaseController
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserAuthenticationService _authenticationService;
        private readonly IOptions<IdentityOptions> _identityOptions;
        public AuthenticationController(IJwtTokenService jwtTokenService, IUserAuthenticationService authenticationService, IOptions<IdentityOptions> identityOptions)
        {
            _jwtTokenService = jwtTokenService;
            _authenticationService = authenticationService;
            _identityOptions = identityOptions;
        }


        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> Login([FromBody]LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authenticationService.PasswordSignInAsync(model.UserName, model.Password, true);

            if (result.Succeeded)
            {
                var token = _jwtTokenService.GenerateToken(result.UserIdentity, result.Roles);

                return Ok(token);
            }

            if (result.IsLockedOut)
            {
                return BadRequest($"User account locked out, max failed access attemps are {_identityOptions.Value.Lockout.MaxFailedAccessAttempts}");
            }
            else if (result.IsNotAllowed)
            {
                return BadRequest("User account is not allowed, make sure your account have been verified");
            }
            else if (result.RequiresTwoFactor)
            {
                return BadRequest("Two Factor Login is required");
            }

            return BadRequest("User Name or Password does not match");
        }
    }
}