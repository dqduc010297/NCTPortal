using Domains.GoGo.Entities.Fleet;
using Groove.AspNetCore.EntityFramework;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructures.EntityConfigurations.GoGo.Fleet_management
{
    class VehicleFeatureConfig : EntityConfiguration<VehicleFeature>
	{		
		public override void OnConfigure(EntityTypeBuilder<VehicleFeature> builder)
		{
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Id).ValueGeneratedOnAdd();

			builder.Property(p => p.FeatureName).IsRequired();
		}
	}
}
