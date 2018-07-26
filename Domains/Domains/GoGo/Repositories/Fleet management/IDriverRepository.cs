using Domains.Core;
using Domains.GoGo.Models.Fleet_management;
using Domains.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domains.GoGo.Repositories.Fleet_management
{
    public interface IDriverRepository
    {
        Task<DriverModel> GetDriverDetail(string id);
        Task<IEnumerable<DataSourceValue<long>>> GetDataSource(string userName);
    }
}
