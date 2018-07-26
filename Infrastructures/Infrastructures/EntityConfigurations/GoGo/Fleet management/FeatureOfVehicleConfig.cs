using Groove.AspNetCore.EntityFramework;
using System;

using System.Collections.Generic;
using System.Text;
using Domains.GoGo.Entities.Fleet;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.EntityConfigurations.GoGo.Fleet_management
{
	public class FeatureOfVehicleConfig : EntityConfiguration<FeatureOfVehicle>
	{
		public override void OnConfigure(EntityTypeBuilder<FeatureOfVehicle> builder)
		{
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Id).ValueGeneratedOnAdd();

			builder.HasOne(p => p.VehicleFeature).WithMany().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
			builder.HasOne(p => p.Vehicle).WithOne().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);


		}
	}
}
