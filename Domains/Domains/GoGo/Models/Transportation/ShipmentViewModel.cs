using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.GoGo.Models
{
    public class ShipmentViewModel
    {
        public int Id { set; get; }
        public string Code { set; get; }
        public string LicensePlate { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public int VehicleID { set; get; }
        public int RequestQuality { set; get; }
        public int PackageQuality { set; get; }
        public string Status { set; get; }
        public string CurrentRequest { set; get; }
    }
}
