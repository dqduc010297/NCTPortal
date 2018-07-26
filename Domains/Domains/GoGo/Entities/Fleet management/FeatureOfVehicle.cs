using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.GoGo.Entities.Fleet
{
    public class FeatureOfVehicle
    {
		public int Id { get; set; }

		public int VehicleFeatureId { get; set; }
		public int VehicleId { get; set; }

		public VehicleFeature VehicleFeature { get; set; }
		public Vehicle Vehicle { get; set; }
	}
}
