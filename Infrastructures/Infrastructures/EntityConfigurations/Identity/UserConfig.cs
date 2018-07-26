using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domains.Identity.Entities;
using Groove.AspNetCore.EntityFramework;

namespace Infrastructures.EntityConfigurations
{
	public class UserConfig : EntityConfiguration<User>
	{
		public override void OnConfigure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Id).ValueGeneratedOnAdd();

			builder.Property(p => p.RowVersion).IsConcurrencyToken();

			builder.Property(p => p.CreatedByUserName).IsRequired();
			builder.Property(p => p.CreatedDate).IsRequired();

			builder.Property(p => p.UpdatedByUserName).IsRequired();
			builder.Property(p => p.UpdatedDate).IsRequired();

		}
	}
}
