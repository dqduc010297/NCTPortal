using Groove.AspNetCore.Common.Identity;
using Groove.AspNetCore.Mvc.Jwt;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Groove.AspNetCore.Mvc.JwtBearer
{
    public interface IJwtTokenService
    {
        JwtToken GenerateToken<TKey>(
            UserIdentity<TKey> userIdentity,
            IEnumerable<string> roles = null,
            IEnumerable<Claim> additionClaims = null,
            string issuer = null,
            string audience = null);
    }
}
