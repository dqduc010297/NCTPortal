using Domains.GoGo.Entities;
using Domains.GoGo.Models.Fleet_management;
using Domains.GoGo.Models;
using Domains.GoGo.Models.Transportation;
using Groove.AspNetCore.UnitOfWork;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domains.GoGo.Repositories.Transportation
{
	public interface IShipmentRepository : IGenericRepository<Shipment, int>
    {
        
        //Task<IEnumerable<ShipmentAssignedModel>> GetShipmentAssignedModel(long? id);
        Task<int> ChangeShipmentStatusById(string id, string status);

		DataSourceResult GetAllAsync(DataSourceRequest request, string userId);
		ShipmentDetailModel GetShipmentById(string id);
		Shipment GetShipment(string id);
		Task<string> ChangeDeliveryStatus(string code, string status);
		// Task<IEnumerable<ShipmentViewModel>> GetShipmentAssignedModel(long? id);
		Task<ShipmentViewModel> GetShipmentAsync(string code);
	}
}
