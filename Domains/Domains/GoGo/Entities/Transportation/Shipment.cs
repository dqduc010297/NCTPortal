using Domains.Identity.Entities;
using Groove.AspNetCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.GoGo.Entities
{
    public class Shipment : IEntity<int>
    {
		public int Id { get; set; }
        public string Code { set; get; }

		public int RequestQuantity { get; set; }

        public DateTime CreatedDate { set; get; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

        public string Status { set; get; }

		public int VehicleId { get; set; }
		public long DriverId { get; set; }
		public long CoordinatorId { get; set; }

		public Vehicle Vehicle { get; set; }
		public User Driver { get; set; }
		public User Coordinator { get; set; }
	}
}
