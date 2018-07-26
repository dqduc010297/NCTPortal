using Domains.GoGo.Entities;
using Groove.AspNetCore.EntityFramework;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructures.EntityConfigurations.GoGo.Fleet_management
{
	class VehicleTypeConfig : EntityConfiguration<VehicleType>
	{
		public override void OnConfigure(EntityTypeBuilder<VehicleType> builder)
		{
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Id).ValueGeneratedOnAdd();

			builder.Property(p => p.TypeName).IsRequired();
		}
	}
}
