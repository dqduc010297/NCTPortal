using Domains.Identity.Entities;
using Groove.AspNetCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.GoGo.Entities
{
    public class ShipmentRequest : IEntity<int>
    {
		public int Id { get; set; }

		public int RequestOrder { get; set; }

		public string Status { get; set; }
		public string Note { get; set; }

		public int ShipmentId { get; set; }
		public int RequestId { get; set; }
        public bool IsProblem { set; get; }
		public DateTime RequestEstimateDate { set; get; }
        public DateTime RequestDeliveriedDate { set; get; }

		public Shipment Shipment { get; set; }
		public Request Request { get; set; }
		
	}
}
