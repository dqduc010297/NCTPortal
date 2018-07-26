using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domains.Core;
using Domains.GoGo.Entities;
using Domains.GoGo.Entities.Fleet;
using Domains.GoGo.Models.Fleet_management;
using Domains.GoGo.Models.Transportation;
using Domains.GoGo.Repositories.Transportation;
using Domains.Helpers;
using Groove.AspNetCore.Common.Identity;
using Groove.AspNetCore.UnitOfWork;
using Kendo.Mvc.UI;

namespace Domains.GoGo.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _repository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

		public RequestService(IMapper mapper, IUnitOfWork uow, IRequestRepository repository)
        {
            _uow = uow;
            _repository = repository;
            _mapper = mapper;
        }


		public DataSourceResult GetAllAsync([DataSourceRequest]DataSourceRequest request)
		{
			return _repository.GetAllAsync(request);
		}

        public Task<RequestDetailModel> GetRequestDetails(int? id)
        {
            return _repository.GetRequestDetailAsync(id);
        }

        //V
        public Task<IEnumerable<RequestsModel>> GetAllAsyncByWarehouseId(string warehouseId)
        {
            return _repository.GetAllAsyncByWareHouseId(warehouseId);
        }

        public Task<RequestsModel> GetRequestByIdAsync(string id)
        {
            return _repository.GetRequestByIdAsync(id);
        }

		public async Task<IEnumerable<DataSourceValue<int>>> GetDataSource(string value, int warehouseId)
		{
			return await _repository.GetDataSource(value, warehouseId);
		}

		public IEnumerable<RequestsModel> GetRequestsByShipmentId(int shipmentId)
		{
			return _repository.GetRequestsByShipmentId(shipmentId);
		}

		public IEnumerable<int> GetRequestIdList(int shipmentId)
		{
			return _repository.GetRequestIdList(shipmentId);
		}

        public async Task<int> GetRequestID(string code)
        {
            return await _repository.GetRequestID(code);
        }

        public Task<LocationModel> GetPositionWarehouse(string code)
        {
            return _repository.GetPositionWarehouseAsync(code);
        }


        //Đ
        // For Customer to create request
        public async Task<int> CreateCustomerRequest(CustomerRequestModel model, long userId)
		{
			var entity = this._mapper.Map<Request>(model);

			// TODO: strings should be managed in constant class or enum
			// Create RequestStatus constant class
			// Then use 
			// entity.Status = RequestStatus.InActive;
            // DONE

			entity.Status = RequestStatus.INACTIVE;
			entity.CreatedDate = DateTime.Now;
			entity.Code = Helper.GenerateCode(DateTime.Now, 1);
			entity.IssuerId = userId; 
			entity.CustomerId = userId;
			entity.WareHouse = null;
            // Step 2: Add request details
            // Save to FeatureOfVehicle
            //var featureEnity = new VehicleFeatureRequest()
            //{
            //    RequestId = entity.Id,
            //    VehicleFeatureId = model.VehicleFeature.Value
            //};

            //_vehicleFeatureRequestRepository.Create(featureEnity);

            //await _uow.SaveChangesAsync();
			_repository.Create(entity);
			await _uow.SaveChangesAsync();
			return entity.Id;
		}

        // For Customer to update request
        public async Task<int> UpdateCustomerRequest(CustomerRequestModel model, long userId)
        {
            var entity = _repository.GetEntityById(model.Id);
            _mapper.Map(model, entity);
            entity.WareHouse = null;
            _repository.Update(entity);
            await _uow.SaveChangesAsync();
            return model.Id;
        }

        // For Customer to get list of requests
        public DataSourceResult GetCustomerRequests(DataSourceRequest request, long userId, string role)
        {
            return _repository.GetCustomerRequestsAsync(request, userId, role);
        }

        // For Customer to get request detail
        public async Task<CustomerRequestModel> FindCustomerRequestAsync(int requestId, long userId, string role)
        {
            return await _repository.FindCustomerRequestAsync(requestId, userId, role);
        }

        // For Customer to change request status
        public Task<string> ChangeStatus(int requestId, string status)
        {
            return this._repository.ChangeStatusAsync(requestId, status);
        }
        //End Đ
    }    
}
