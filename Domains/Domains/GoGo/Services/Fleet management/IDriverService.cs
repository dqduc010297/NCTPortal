using Domains.Core;
using Domains.GoGo.Models.Fleet_management;
using Domains.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domains.GoGo.Services.Fleet_management
{
    public interface IDriverService 
    {
        Task<DriverModel> GetDriverDetail(string id);
        Task<IEnumerable<DataSourceValue<long>>> GetDataSource(string userName);
    }
}
