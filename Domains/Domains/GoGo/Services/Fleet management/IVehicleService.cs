using Domains.Core;
using Domains.GoGo.Entities;
using Domains.GoGo.Models.Fleet_management;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domains.GoGo.Services.Fleet_management
{
    public interface IVehicleService
    {
        Task<VehicleModel> GetVehicleDetailAsync(int id);
        Task<IEnumerable<DataSourceValue<int>>> GetDataSource(string licensePlate);
    }
}
