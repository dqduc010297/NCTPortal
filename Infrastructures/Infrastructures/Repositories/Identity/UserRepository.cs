using System;
using System.Collections.Generic;
using System.Text;
using Domains.Identity.Entities;
using Domains.Identity.Repositories;
using Groove.AspNetCore.UnitOfWork.EntityFramework;
using Infrastructures;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domains.Identity.Models;
using Groove.AspNetCore.DataBinding.AutoMapperExtentions;
using AutoMapper;
using Groove.AspNetCore.UnitOfWork;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Domains.Core;


namespace Infrastructures.Repositories.Identity
{
    public class UserRepository : GenericRepository<User, long>, IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public UserRepository(
            UserManager<User> userManager,
            ApplicationDbContext dbContext,
            IMapper mapper)
            : base(dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<User> FindByUserNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<UserReadModel> FindByUserIdAsync(long? id)
        {
            //return await _userManager.FindByIdAsync(id);
            var user = await _userManager.FindByIdAsync(id.ToString());
            var userMap = _mapper.Map<UserReadModel>(user);
            var role = await _userManager.GetRolesAsync(user);
            var roleMap = new
            {
                userMap.Id,
                userMap.UserName,
                userMap.Email,
                userMap.PhoneNumber,
                Role = role[0],
                userMap.CreatedDate
            };
            return _mapper.Map<UserReadModel>(roleMap);

            //return await this.dbSet.Where(p => p.Id == id).MapQueryTo<UserReadModel>(_mapper).FirstAsync();
        }

        #region Old feature of find the user to edit in list by id
        //Find the user to edit in list by id
        //public async Task<UserViewUpdateModel> GetUserUpdateByIdAsync(long? id)
        //{
        //    var user = await _userManager.FindByIdAsync(id.ToString());
        //    var userMap = _mapper.Map<UserViewUpdateModel>(user);
        //    var role = await _userManager.GetRolesAsync(user);
        //    var roleMap = new
        //    {
        //        userMap.Id,
        //        userMap.UserName,
        //        userMap.Email,
        //        userMap.PhoneNumber,
        //        Role = role[0]
        //    };
        //    return _mapper.Map<UserViewUpdateModel>(roleMap);
        //}
        #endregion

        #region Old feature of get user list (use original table)
        //public async Task<IEnumerable<UserListModel>> GetUserListAsync()
        //{
        //    //Get all user ID in table userroles
        //    //var userIds = context.Set<IdentityUserRole<long>>()
        //    //                .Where(a => a.RoleId == id)
        //    //                .ToList();

        //    //Find all users with specific role
        //    //return await context.Set<User>()
        //    //                .Where(a => userIds.Any(c => c.UserId == a.Id))
        //    //                .MapQueryTo<UserListModel>(_mapper)
        //    //                .ToListAsync();

        //    //Get all users use below code
        //    var user = await _userManager.Users.ToListAsync();
        //    List<UserListModel> userList = new List<UserListModel>();
        //    for (int i = 1; i < user.Count; i++)
        //    {
        //        var role = await _userManager.GetRolesAsync(user[i]);
        //        userList.Add(new UserListModel
        //        {
        //            Id = user[i].Id,
        //            UserName = user[i].UserName,
        //            Email = user[i].Email,
        //            PhoneNumber = user[i].PhoneNumber,
        //            Role = role[0],
        //            Status = user[i].Status
        //        });
        //    }

        //    return userList;

        //    //return await _userManager.Users.MapQueryTo<UserListModel>(_mapper).ToListAsync();
        //}
        #endregion

        public DataSourceResult GetUserListAsync(DataSourceRequest request)
        {
            //Get all users include role name, use below code
            var query = (from uRole in _dbContext.UserRoles
                         from user in _userManager.Users
                         from role in _dbContext.Roles
                         where uRole.UserId == user.Id && role.Id == uRole.RoleId
                         select new UserListModel
                         {
                             Id = user.Id,
                             UserName = user.UserName,
                             Email = user.Email,
                             PhoneNumber = user.PhoneNumber,
                             Role = role.Name,
                             Status = user.Status
                         });

            return query.ToDataSourceResult(request);
        }
    }
}
