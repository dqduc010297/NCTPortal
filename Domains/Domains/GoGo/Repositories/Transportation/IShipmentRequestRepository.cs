using Domains.GoGo.Entities;
using Domains.GoGo.Models.Transportation;
using Groove.AspNetCore.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domains.GoGo.Repositories.Transportation
{
    public interface IShipmentRequestRepository : IGenericRepository<ShipmentRequest, int>
    {
        Task<LocationModel> GetPositionPickingAsync(string code);
        int GetTotalRequest(string code);
        RequestDetailModel GetRequestDetailModel(string code);
        Task<RequestDetailModel> GetCurrentRequestAsync(string requestCode);
        Task<string> GetFirstRequestCode(string shipmentCode);
        Task<IEnumerable<RequestDetailModel>> GetRequestListAsync(string code);
        Task<string> ChangeStatusRequestAsync(string code, string status);
        Task<string> Problem(string requestCode, bool status);
  
		void UpdateShipmentRequest(List<int> requestIdList, int shipmentId);
        string GetRequestStatus(int requestId, int userId);
    }
}
