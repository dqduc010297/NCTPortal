using Domains.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domains.GoGo.Services.Fleet_management
{
    public interface IVehicleFeatureService
    {
        Task<IEnumerable<DataSourceValue<int>>> GetOnFilter(string displayName);
    }
}
