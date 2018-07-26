using Domains.Core;
using Domains.GoGo.Entities.Fleet;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domains.GoGo.Services.Transportation
{
    public interface IVehicleFeatureRequestService
    {
        Task<int> CreateVehicleFeatureRequest(int requestId, int vehicleFeatureId);
        DataSourceValue<int> FindVehicleFeature(int requestId);
        Task<int> UpdateVehicleFeatureAsync(int requestId, int vehicleFeatureId);
    }
}
