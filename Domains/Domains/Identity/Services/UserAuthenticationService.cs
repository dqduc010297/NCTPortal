using Domains.Identity.Entities;
using Domains.Identity.Models;
using Domains.Identity.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Identity.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public UserAuthenticationService(IUserRepository userRepository, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<SignInResultModel> PasswordSignInAsync(string userName, string password, bool lockoutOnFailure)
        {

            var user = await _userRepository.FindByUserNameAsync(userName);

            if (user == null)
            {
                return new SignInResultModel
                {
                    Succeeded = false
                };
            }


            var loginResult = new SignInResultModel(await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure));
            if (!loginResult.Succeeded)
            {
                return loginResult;
            }

            loginResult.Roles = await _userManager.GetRolesAsync(user);
            loginResult.UserIdentity = new Groove.AspNetCore.Common.Identity.UserIdentity<long>
            {
                Id = user.Id,
                UserName = user.UserName
            };
            return loginResult;
        }
    }
}
