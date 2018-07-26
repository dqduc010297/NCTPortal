using Domains.GoGo;
using Groove.AspNetCore.EntityFramework;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructures.EntityConfigurations.GoGo
{
	public class WareHouseConfig : EntityConfiguration<WareHouse>
	{
		public override void OnConfigure(EntityTypeBuilder<WareHouse> builder)
		{
			builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasIndex(p => p.NameWarehouse).IsUnique();
            builder.Property(p => p.NameWarehouse).IsRequired();

            builder.Property(p => p.PhoneNumber).HasMaxLength(20).IsRequired();
			builder.Property(p => p.Latitude).IsRequired();
            builder.Property(p => p.Longitude).IsRequired();
        }
	}
}
