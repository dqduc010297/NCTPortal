using Domains.GoGo.Entities.Fleet;
using Groove.AspNetCore.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domains.GoGo.Repositories.Transportation
{
    public interface IVehicleFeatureRequestRepository : IGenericRepository<VehicleFeatureRequest, int>
    {
        VehicleFeatureRequest FindVehicleFeatureAsync(int requestId);
        Task<VehicleFeatureRequest> GetByRequestIdAsync(int requestId);
    }
}
