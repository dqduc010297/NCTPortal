using AutoMapper;
using Domains.Core;
using Domains.GoGo.Models;
using Domains.Identity.Entities;
using Groove.AspNetCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.GoGo
{
    public class WareHouse : IEntity<int>
    {
        public int Id { get; set; }

        public string NameWarehouse { set; get; }
        public string PhoneNumber { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { set; get; }

        public long OwnerId { get; set; }
		public User Owner { get; set; }		
	}
}
