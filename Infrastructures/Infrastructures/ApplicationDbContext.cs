using System;
using System.Collections.Generic;
using System.Text;
using Domains.Identity.Entities;
using Groove.AspNetCore.EntityFramework;
using Groove.AspNetCore.UnitOfWork;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures
{
	public class ApplicationDbContext : IdentityDbContext<User, Role, long>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public object GetUnitOfWorkContext()
		{
			return this;
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			var configLoader = new ConfigurationLoader<ApplicationDbContext>(builder);
			configLoader.Load();
		}
	}
}
