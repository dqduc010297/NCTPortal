using System;
using Groove.AspNetCore.Common.Identity;
using Groove.AspNetCore.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Domains.Identity.Entities
{
    public class User : IdentityUser<long>, IVersionedEntity<long>
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }

		public DateTimeOffset CreatedDate { get; set; }
		public long CreatedByUserId { get; set; }     // use to query/join
		public string CreatedByUserName { get; set; } // Use to display

		public DateTimeOffset UpdatedDate { get; set; }
		public long UpdatedByUserId { get; set; } // use to query/join
		public string UpdatedByUserName { get; set; } // Use to display
        
		public byte[] RowVersion { get; set; }
        public string Status { set; get; }

        public User CreateBy(UserIdentity<long> issuer)
		{
			var now = DateTimeOffset.UtcNow;

			CreatedByUserId = issuer.Id;
			CreatedByUserName = issuer.UserName;
			CreatedDate = now;

			return this;
		}

		public User UpdateBy(UserIdentity<long> issuer)
		{
			var now = DateTimeOffset.UtcNow;

			UpdatedByUserId = issuer.Id;
			UpdatedByUserName = issuer.UserName;
			UpdatedDate = now;

			return this;
		}


	}
}
