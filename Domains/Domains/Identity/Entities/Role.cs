using System;
using Groove.AspNetCore.Domain.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Domains.Identity.Entities
{
	public class Role : IdentityRole<long>, IVersionedEntity<long>
	{
		public DateTimeOffset CreatedDate { get; set; }
		public long CreatedByUserId { get; set; }     // use to query/join
		public string CreatedByUserName { get; set; } // Use to display

		public DateTimeOffset ModifiedDate { get; set; }
		public long ModifiedByUserId { get; set; }     // use to query/join
		public string ModifiedByUserName { get; set; } // Use to display
		public byte[] RowVersion { get; set; }
	}
}
