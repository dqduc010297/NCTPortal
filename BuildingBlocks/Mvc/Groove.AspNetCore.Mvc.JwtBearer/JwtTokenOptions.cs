using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Groove.AspNetCore.Mvc.JwtBearer
{
    public class JwtTokenOptions
    {
        public SecurityKey SecurityKey { get; set; }
        public long TokenTimeOutMinutes { get; set; }

        public bool ValidateAudience { get; set; } = false;
        public bool ValidateIssuer { get; set; } = false;
        public bool ValidateIssuerSigningKey { get; set; } = true;
        public bool ValidateLifetime { get; set; } = true;
    }
}
