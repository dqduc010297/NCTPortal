using Domains.GoGo.Entities;
using Groove.AspNetCore.EntityFramework;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructures.EntityConfigurations.GoGo
{
	public class ShipmentConfig : EntityConfiguration<Shipment>
	{

		public override void OnConfigure(EntityTypeBuilder<Shipment> builder)
		{
			builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasIndex(p => p.Code).IsUnique();

			builder.HasOne(p => p.Vehicle).WithMany().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
			builder.HasOne(p => p.Driver).WithMany().HasForeignKey(p => p.DriverId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
			builder.HasOne(p => p.Coordinator).WithMany().HasForeignKey(p => p.CoordinatorId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

		}
	}
}
