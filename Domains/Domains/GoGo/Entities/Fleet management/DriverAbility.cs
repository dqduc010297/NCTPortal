using Domains.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.GoGo.Entities
{
    public class DriverAbility
    {
		public int Id { get; set; }

		public int VehicleTypeId { get; set; }
		public long DriverId { get; set; }

		public User Driver { get; set; }
		public VehicleType VehicleType { get; set; }
	}
}
