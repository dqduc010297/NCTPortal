using System;
using System.Collections.Generic;
using System.Text;

namespace Groove.AspNetCore.Mvc.Jwt
{
    public class JwtToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public double expires_in { get; set; } // Seconds

        public DateTimeOffset expires_date { get; set; }
        public DateTimeOffset issue_date { get; set; }
    }
}
