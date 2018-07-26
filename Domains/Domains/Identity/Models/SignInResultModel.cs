using Groove.AspNetCore.Common.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Identity.Models
{
    public class SignInResultModel
    {
        public SignInResultModel()
        {
        }
        public SignInResultModel(SignInResult originResult)
        {
            this.Succeeded = originResult.Succeeded;
            this.IsLockedOut = originResult.IsLockedOut;
            this.RequiresTwoFactor = originResult.RequiresTwoFactor;
        }
        public bool Succeeded { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsNotAllowed { get; set; }
        public bool RequiresTwoFactor { get; set; }

        public IEnumerable<string> Roles { get; set; }
        public UserIdentity<long> UserIdentity { get; set; }
    }
}
