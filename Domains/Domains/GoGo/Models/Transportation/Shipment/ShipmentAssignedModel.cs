using AutoMapper;
using Domains.GoGo.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.GoGo.Models.Transportation
{
    public class ShipmentAssignedModel
    {
        public string Code { set; get; }
        public string LicensePlate { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public int VehicleID { set; get; }
        public int RequestQuality { set; get; }
        public string Status { set; get; }
    }
  
}
