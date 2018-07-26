using Domains.Identity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Identity.Services
{
    public interface IUserAuthenticationService
    {
        Task<SignInResultModel> PasswordSignInAsync(string userName, string password, bool lockoutOnFailure);
    }
}
