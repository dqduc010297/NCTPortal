using Domains.Core;
using Domains.GoGo.Entities;
using Domains.GoGo.Models.Fleet_management;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domains.GoGo.Repositories.Fleet_management
{
    public interface IVehicleRepository
    {
        Task<VehicleModel> GetVehicleDetailAsync(int id);
        Task<IEnumerable<DataSourceValue<int>>> GetDataSource(string licensePlate);
    }
}
