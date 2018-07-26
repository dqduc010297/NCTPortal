using Groove.AspNetCore.EntityFramework;
using System;
using Domains.GoGo.Entities;
using System.Collections.Generic;
using System.Text;

namespace Infrastructures.EntityConfigurations.GoGo
{
	public class RequestConfig : EntityConfiguration<Request>
	{
		public override void OnConfigure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Request> builder)
		{
			builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

			builder.HasOne(p => p.Issuer).WithMany().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
			builder.HasOne(p => p.WareHouse).WithMany().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
            builder.HasOne(p => p.Customer).WithMany().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
            builder.HasIndex(p => p.Code).IsUnique();

			builder.Property(p => p.WareHouseId).IsRequired();
			builder.Property(p => p.DeliveryLatitude).IsRequired();
            builder.Property(p => p.DeliveryLongitude).IsRequired();
            builder.Property(p => p.Status).IsRequired();
		}
	}
}
