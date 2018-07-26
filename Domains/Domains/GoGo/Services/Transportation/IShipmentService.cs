using Domains.GoGo.Models;
using Domains.GoGo.Models.Transportation;
using Groove.AspNetCore.Common.Identity;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domains.GoGo.Services.Transportation
{
    public interface IShipmentService
    {
		Task<int> CreateShipmentAsync(FormShipmentModel model);
		DataSourceResult GetAllAsync([DataSourceRequest]DataSourceRequest request, string userId);

       // Task<IEnumerable<ShipmentAssignedModel>> GetShipmentAssignedModel(long? id);
		Task<int> ChangeShipmentStatusById(string id, string status);
		ShipmentDetailModel GetShipmentById(string id);
		Task UpdateShipmentByIdAsync( string code, FormShipmentModel model);

        Task<string> ChangeDeliveryStatus(string code, string status);
        //   Task<IEnumerable<ShipmentViewModel>> GetShipmentAssignedModel(long? id);
        Task<ShipmentViewModel> GetShipmentAsync(string code);
    }
}
