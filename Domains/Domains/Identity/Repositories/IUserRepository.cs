using Domains.Identity.Entities;
using Domains.Identity.Models;
using Groove.AspNetCore.UnitOfWork;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Identity.Repositories
{
    public interface IUserRepository : IGenericRepository<User, long>
    {
        Task<User> FindByUserNameAsync(string userName);
        DataSourceResult GetUserListAsync(DataSourceRequest request);
        Task<UserReadModel> FindByUserIdAsync(long? id);
        #region Old get user list with specific role by role id
        // Get user list with specific role by role id
        //Task<IEnumerable<UserListModel>> GetUserListAsync(long? id);
        //Task<IEnumerable<UserListModel>> GetUserListAsync();
        #endregion
        #region Old feature of get user to edit in list by id
        //Task<UserViewUpdateModel> GetUserUpdateByIdAsync(long? id);
        #endregion
    }
}
