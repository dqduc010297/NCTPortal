using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Core
{
   public class ShipmentStatus
   {
		public const string INACTIVE = "Inactive";
		public const string WAITING =  "Waiting";
		public const string ACCEPTED = "Accepted";
		public const string REJECTED = "Rejected";
		public const string Picking = "Picking";
		public const string LOADING = "Loading";
		public const string SHIPPING = "Shipping";
		public const string COMPLETED = "Completed";
	}

    public class ShipmentRequestStatus
    {
        public const string INACTIVE = "Inactive";
        public const string WAITING = "Waiting";
        public const string SHIPPING = "Shipping";
        public const string UNLOADING = "Unloading";
        public const string COMPLETED = "Completed";

        //Another path
        public const string CREATED = "Created";
        public const string UPDATED = "Updated";

    }

    public class RequestStatus
	{
		public const string INACTIVE = "Inactive";
		public const string SENDING =  "Sending";
		public const string ACCEPTED =  "Accepted";
		public const string REJECTED =  "Rejected";
    }
}
