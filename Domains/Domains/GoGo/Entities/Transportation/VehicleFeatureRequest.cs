using Groove.AspNetCore.Domain.Entities;
using System.Collections.Generic;
using System.Text;

namespace Domains.GoGo.Entities.Fleet
{
    public class VehicleFeatureRequest : IEntity<int>
    {
		public int Id{ get; set; }

		public int RequestId { get; set; }
		public int VehicleFeatureId { get; set; }

		public Request Request { get; set; }
		public VehicleFeature VehicleFeature { get; set; }
	}
}
