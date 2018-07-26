using System;
using System.Collections.Generic;
using System.Text;
using Domains.Core;
using Groove.AspNetCore.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.EntityConfigurations
{
	public abstract class EntityConfigurationBase<TEntity, TKeyType> : EntityConfiguration<TEntity>
		where TEntity : EntityBase<TKeyType>
	{
		public new void Configure(ModelBuilder builder)
		{
			EntityTypeBuilder<TEntity> typeBuilder = builder.Entity<TEntity>();

			typeBuilder.HasKey(p => p.Id);
			typeBuilder.Property(p => p.Id).ValueGeneratedOnAdd();

			typeBuilder.Property(p => p.RowVersion).IsConcurrencyToken();

			typeBuilder.Property(p => p.CreatedByUserName).IsRequired();
			typeBuilder.Property(p => p.CreatedDate).IsRequired();

			typeBuilder.Property(p => p.UpdatedByUserName).IsRequired();
			typeBuilder.Property(p => p.UpdatedDate).IsRequired();

			OnConfigure(typeBuilder);
		}

	}
}
