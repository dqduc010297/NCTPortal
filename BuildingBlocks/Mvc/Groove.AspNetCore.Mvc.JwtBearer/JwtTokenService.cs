using Groove.AspNetCore.Common.Identity;
using Groove.AspNetCore.Mvc.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Groove.AspNetCore.Mvc.Jwt
{
    public class JwtTokenService : IJwtTokenService
    {
        private JwtTokenOptions options;
        public JwtTokenService(JwtTokenOptions options)
        {
            this.options = options;
        }
        public JwtToken GenerateToken<TKey>(
            UserIdentity<TKey> userIdentity,
            IEnumerable<string> roles = null,
            IEnumerable<Claim> additionClaims = null,
            string issuer = null,
            string audience = null)
        {
            var userIdInString = userIdentity.Id.ToString();
            // Token expired date
            var timeout = TimeSpan.FromMinutes(this.options.TokenTimeOutMinutes);
            var issueDate = DateTime.UtcNow;
            var expiredDate = issueDate.Add(timeout);

            // Token's claims

            var claims = new List<Claim>();

            if (additionClaims != null)
            {
                claims.AddRange(additionClaims);
            }

            if (roles != null)
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }
            claims.Add(new Claim(ClaimTypes.Name, userIdentity.UserName));           // Is unique name of user, which will be passed to System.Web.HttpContext.Current.User.Identity.Name, If you gonna use a logger, this name will be passed to logger as an identity
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userIdInString));        // Identity of user, which will be used to store user id, you would need to store user id in the token to prevent round-trip to the Data-Store

            // Token crypto
            var signingCredential = new SigningCredentials(this.options.SecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtPayload = new JwtPayload(issuer: issuer, audience: audience, claims: claims, notBefore: null, expires: expiredDate, issuedAt: issueDate);
            var jwtHeader = new JwtHeader(signingCredential);
            var jwtToken = new JwtSecurityToken(jwtHeader, jwtPayload);

            return new JwtToken
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                expires_in = timeout.TotalSeconds,
                expires_date = expiredDate,
                issue_date = issueDate,
                token_type = "Bearer"
            };
        }
    }
}
