using Domains.Core;
using Domains.GoGo.Models.Transportation;
using Domains.Helpers;
using Kendo.Mvc.UI;
using Groove.AspNetCore.Common.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domains.GoGo.Services
{
	public interface IRequestService
    {
		Task<RequestDetailModel> GetRequestDetails(int? id);
		Task<IEnumerable<DataSourceValue<int>>>  GetDataSource(string value, int warehouseId);
        Task<IEnumerable<RequestsModel>> GetAllAsyncByWarehouseId(string warehouseId);
        DataSourceResult GetAllAsync([DataSourceRequest]DataSourceRequest request);
        Task<RequestsModel> GetRequestByIdAsync(string id);
        Task<LocationModel> GetPositionWarehouse(string code);
        Task<int> GetRequestID(string code);


        // Đ
        Task<CustomerRequestModel> FindCustomerRequestAsync(int id, long userId, string role);
        DataSourceResult GetCustomerRequests(DataSourceRequest request, long userId, string role);
        Task<int> CreateCustomerRequest(CustomerRequestModel model, long userId);
        Task<int> UpdateCustomerRequest(CustomerRequestModel model, long userId);
        Task<string> ChangeStatus(int requestId, string status);
        // END Đ
    }
}
	