using Domains.Core;
using Domains.GoGo.Entities;
using Domains.GoGo.Models.Transportation;
using Kendo.Mvc.UI;
using Groove.AspNetCore.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domains.GoGo.Repositories.Transportation
{
    public interface IRequestRepository : IGenericRepository<Request, int>
    {
		DataSourceResult GetAllAsync([DataSourceRequest]DataSourceRequest request);

		Task<RequestDetailModel> GetRequestDetailAsync(int? id);

		Task<IEnumerable<DataSourceValue<int>>> GetDataSource(string value, int warehouseId);
        Task<IEnumerable<RequestsModel>> GetAllAsyncByWareHouseId(string warehouseId);
        Task<RequestsModel> GetRequestByIdAsync(string code);

		IEnumerable<RequestsModel> GetRequestsByShipmentId(int shipmentId);
		IEnumerable<int> GetRequestIdList(int shipmentId);

	
        Task<LocationModel> GetPositionWarehouseAsync(string code);
        Task<int> GetRequestID(string code);
        //void UpdateCustomerRequest(RequestModel model, long userId);
        //Task<int> CreateCustomerRequest(RequestModel model, long userId);

        // Đ
        Task<CustomerRequestModel> FindCustomerRequestAsync(int requestId, long userId, string role);
        DataSourceResult GetCustomerRequestsAsync(DataSourceRequest request, long userId, string role);
        Task<string> ChangeStatusAsync(int requestId, string status);
        // End Đ
    }
}
