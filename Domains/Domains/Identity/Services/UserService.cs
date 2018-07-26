using AutoMapper;
using Domains.Identity.Entities;
using Domains.Identity.Helper;
using Domains.Identity.Models;
using Domains.Identity.Repositories;
using Groove.AspNetCore.Common.Exceptions;
using Groove.AspNetCore.Common.Identity;
using Groove.AspNetCore.Common.Messages;
using Groove.AspNetCore.UnitOfWork;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManagement;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public UserService(
            IMapper mapper,
            IUserRepository userRepository,
            UserManager<User> userManagement,
            IUnitOfWork uow)
        {
            _userManagement = userManagement;
            _userRepository = userRepository;
            _mapper = mapper;
            _uow = uow;
        }
        
        public async Task<long> CreateUserAsync(UserCreateModel model, UserIdentity<long> issuer)
        {
            // TODO: values should be managed in Constant or enum
            // Create UserStatus class to manage user status
            // then use: 
            // model.Status = UserStatus.Active

            model.Status = UserStatus.Active;

            var user = _mapper.Map<User>(model);
            user.CreateBy(issuer).UpdateBy(issuer);

            var identityResult = await _userManagement.CreateAsync(user, model.Password);

            if (!identityResult.Succeeded)
            {
                throw CreateException(identityResult.Errors);
            }

            IdentityResult resultRole = await _userManagement.AddToRoleAsync(user, model.Role);

            return user.Id;
        }

        public async Task<long> UpdateUserAsync(long id, UserUpdateModel model, UserIdentity<long> issuer)
        {
            var user = _userRepository.GetEntityById(id);
            var role = await _userManagement.GetRolesAsync(user);

            _mapper.Map(model, user);
            user.UpdateBy(issuer);

            if (role[0] != model.Role)
            {
                var identityResult = await _userManagement.RemoveFromRolesAsync(user, role);
                if (identityResult.Succeeded)
                {
                    await _userManagement.AddToRoleAsync(user, model.Role);
                }
            }
            _userRepository.Update(user);
            
            await _uow.SaveChangesAsync();

            return user.Id;
        }

        public async Task<long> UpdateUserProfileAsync(long id, UserProfileUpdateModel model, UserIdentity<long> issuer)
        {
            var user = _userRepository.GetEntityById(id);

            _mapper.Map(model, user);
            user.UpdateBy(issuer);

            _userRepository.Update(user);

            await _uow.SaveChangesAsync();

            return user.Id;
        }

        //Get the user profile
        public Task<UserReadModel> GetUserProfileAsync(long? id)
        {
            return _userRepository.FindByUserIdAsync(id);
        }

        //Get the user detail in list
        public Task<UserReadModel> GetUserDetailAsync(long? id)
        {
            return _userRepository.FindByUserIdAsync(id);
        }

        //Get the value of user need to edit
        //public Task<UserViewUpdateModel> GetUserUpdateAsync(long? id)
        //{
        //    return _userRepository.GetUserUpdateByIdAsync(id);
        //}

        //Get list of user with specific role by role id
        public DataSourceResult GetUsersAsync(DataSourceRequest request)
        {
            return _userRepository.GetUserListAsync(request);
        }

        private UserDefinedException CreateException(IEnumerable<IdentityError> errors)
        {
            var exception = new UserDefinedException();
            exception.UserDefinedMessage = new ExceptionMessage();
            exception.UserDefinedMessage.Details = new List<ExceptionMessage>();

            foreach (var error in errors)
            {
                exception.UserDefinedMessage.Details.Add(new ExceptionMessage
                {
                    Message = error.Description
                });
            }
            exception.UserDefinedMessage.Message = exception.UserDefinedMessage.Details.First().Message;

            return exception;
        }
    }
}
