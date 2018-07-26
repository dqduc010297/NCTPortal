using Domains.Identity.Entities;
using Groove.AspNetCore.Common.Identity;
using Groove.AspNetCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace Domains.Core
{
	public class EntityBase<TKeyType> : IVersionedEntity<TKeyType>
	{

		public TKeyType Id { get; set; }

		public DateTimeOffset CreatedDate { get; set; }
		public long CreatedByUserId { get; set; }     // use to query/join
		public string CreatedByUserName { get; set; } // Use to display

		public DateTimeOffset UpdatedDate { get; set; }
		public long UpdatedByUserId { get; set; } // use to query/join
        public User UpdatedByUser { get; set; }
        public string UpdatedByUserName { get; set; } // Use to display

		public byte[] RowVersion { get; set; }


		public EntityBase<TKeyType> CreateBy(UserIdentity<long> issuer)
		{
			var now = DateTimeOffset.UtcNow;

			CreatedByUserId = issuer.Id;
			CreatedByUserName = issuer.UserName;
			CreatedDate = now;

			return this;
		}

		public EntityBase<TKeyType> UpdateBy(UserIdentity<long> issuer)
		{
			var now = DateTimeOffset.UtcNow;

			UpdatedByUserId = issuer.Id;
			UpdatedByUserName = issuer.UserName;
			UpdatedDate = now;

			return this;
		}
	}
}
