using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Groove.AspNetCore.EntityFramework
{
    public abstract class EntityConfiguration<TEntity> : IEntityConfiguration where TEntity : class
    {
        public void Configure(ModelBuilder builder)
        {
            var typeBuilder = builder.Entity<TEntity>();

            OnConfigure(typeBuilder);
        }

        public abstract void OnConfigure(EntityTypeBuilder<TEntity> builder);

    }
}
