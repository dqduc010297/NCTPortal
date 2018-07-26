using Groove.AspNetCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.GoGo.Entities
{
    public class Vehicle : IEntity<int>
    {
		public int Id { get; set; }

		public string LicensePlate { get; set; }

		public int VehicleTypeId { get; set; }
		
		public float Height { get; set; }
		public float Width { get; set; }
		public float Lenght { get; set; }

		public VehicleType VehicleType { get; set; }
	}
}
