using Domains.GoGo.Entities;
using Groove.AspNetCore.EntityFramework;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructures.EntityConfigurations.GoGo
{
	public class VehicleConfig : EntityConfiguration<Vehicle>
	{		
		public override void OnConfigure(EntityTypeBuilder<Vehicle> builder)
		{
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Id).ValueGeneratedOnAdd();

			builder.HasOne(p => p.VehicleType).WithMany().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

			builder.Property(p => p.LicensePlate).IsRequired();
			builder.HasIndex(p => p.LicensePlate).IsUnique();
		}
	}
}
