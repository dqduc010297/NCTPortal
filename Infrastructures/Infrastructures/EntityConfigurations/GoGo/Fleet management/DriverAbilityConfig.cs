using Domains.GoGo.Entities;
using Groove.AspNetCore.EntityFramework;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructures.EntityConfigurations.GoGo.Fleet_management
{
	class DriverAbilityConfig : EntityConfiguration<DriverAbility>
	{
		public override void OnConfigure(EntityTypeBuilder<DriverAbility> builder)
		{
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Id).ValueGeneratedOnAdd();


			builder.HasOne(p => p.VehicleType).WithMany().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
			builder.HasOne(p => p.Driver).WithMany().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
		}
	}
}
