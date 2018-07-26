using Domains.Core;
using Domains.GoGo.Entities.Fleet;
using Groove.AspNetCore.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domains.GoGo.Repositories.Fleet_management
{
    public interface IVehicleFeatureRepository : IGenericRepository<VehicleFeature, int>
    {
        Task<IEnumerable<DataSourceValue<int>>> GetOnFilter(string displayName);

    }
}
