using Domains.GoGo.Entities;
using Domains.GoGo.Repositories.Transportation;
using Groove.AspNetCore.UnitOfWork;
using Groove.AspNetCore.UnitOfWork.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Groove.AspNetCore.DataBinding.AutoMapperExtentions;
using AutoMapper;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domains.GoGo.Models.Transportation;
using Domains.GoGo;
using Domains.Core;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Domains.GoGo.Models;
using Domains.GoGo.Entities.Fleet;
namespace Infrastructures.Repositories.GoGo.Transportation
{
    public class RequestRepository : GenericRepository<Request, int>, IRequestRepository
    {
        private readonly IMapper _mapper;

        public RequestRepository(IMapper mapper, ApplicationDbContext uoWContext) : base(uoWContext)
        {
            _mapper = mapper;
        }

		public async Task<RequestDetailModel> GetRequestDetailAsync(int? id)
        {
			

            return await this.dbSet.Where(p => p.Id == id).MapQueryTo<RequestDetailModel>(_mapper).FirstAsync();
        }

		public DataSourceResult GetAllAsync([DataSourceRequest] DataSourceRequest request)
		{
			return this.dbSet.MapQueryTo<RequestsModel>(_mapper).ToDataSourceResult(request);
		}

		//V
		public async Task<IEnumerable<DataSourceValue<int>>> GetDataSource(string value, int warehouseId)
		{
            // TODO: Create ShipmentStatus class for Constant instead of hard code
			// Done
            var requestedIdList = this.context.Set<ShipmentRequest>().Where(p => p.Status != ShipmentStatus.INACTIVE).Select(p => p.RequestId).ToList();

            // TODO: Create RequestStatus class for Constant instead of hard code
            return await this.dbSet.Where(p => (( p.Code.Contains(value) || p.Address.Contains(value)) 
									&& !requestedIdList.Contains(p.Id) && p.Status == RequestStatus.SENDING && p.WareHouseId == warehouseId ) )
									.Select(p => new DataSourceValue<int>
									{
										Value = p.Id,
										DisplayName = p.Code
									}).ToListAsync();
		}

        public async Task<RequestsModel> GetRequestByIdAsync(string id)
        {
            return await this.dbSet.Where(p => p.Id.ToString() == id).MapQueryTo<RequestsModel>(_mapper).FirstAsync();
        }

		public IEnumerable<RequestsModel> GetRequestsByShipmentId(int shipmentId)
		{
            // TODO: Create ShipmentRequestStatus class for Constant instead of hard code
			// Done
            var requestIdList = this.context.Set<ShipmentRequest>().Where(p =>( p.ShipmentId == shipmentId && p.Status == ShipmentStatus.WAITING)).Select(p => p.RequestId).ToList();

			return this.dbSet.Where(p => (requestIdList.IndexOf(p.Id) != -1)).MapQueryTo<RequestsModel>(_mapper).ToList();
		}

		public IEnumerable<int> GetRequestIdList(int shipmentId)
		{
            // TODO: Create ShipmentRequestStatus class for Constant instead of hard code
			// Done
            return this.context.Set<ShipmentRequest>().Where(p => (p.ShipmentId == shipmentId && p.Status == ShipmentStatus.WAITING)).Select(p => p.RequestId).ToList();
		}

        public async Task<LocationModel> GetPositionWarehouseAsync(string code)
        {
            var query = this.dbSet
                .Include(p => p.WareHouse)
                .Where(p => p.Code == code)
                .Select(p => new LocationModel
                {
                    Address = p.Address,
                    Latitude = p.WareHouse.Latitude,
                    Longitude = p.WareHouse.Longitude
                });
            return await query.FirstAsync();
        }

        public async Task<int> GetRequestID(string code)
        {
            return await this.dbSet.Where(p => p.Code == code).Select(p => p.Id).FirstAsync();
        }

        public async Task<IEnumerable<RequestsModel>> GetAllAsyncByWareHouseId(string warehouseId)
        {
            var plannedRequestIdList = this.context.Set<ShipmentRequest>().Where(p => p.Status != ShipmentRequestStatus.INACTIVE).Select(p => p.RequestId).ToList();
            return await this.dbSet.Where(p => (!plannedRequestIdList.Contains(p.Id) && (p.Status == RequestStatus.ACCEPTED))).MapQueryTo<RequestsModel>(_mapper).ToListAsync();
        }


        // Đ
        // For Customer to get request list
        public DataSourceResult GetCustomerRequestsAsync(DataSourceRequest request, long userId, string role)
        {
            if (role == "Customer")
            {
                return this.dbSet.Include(p => p.WareHouse).Where(p => p.CustomerId == userId).OrderByDescending(p => p.CreatedDate).Select(p => new SummaryRequestModel
                {
                    Id = p.Id,
                    WareHouse = p.WareHouse.Address,
                    ExpectedDate = p.ExpectedDate,
                    Address = p.Address,
                    Status = p.Status,
                    Code = p.Code,
                    PickingDate = p.PickingDate,
                }).ToDataSourceResult(request);
            }
            else
            {
                return this.dbSet.Include(p => p.WareHouse).Where(p => p.Status != RequestStatus.INACTIVE).OrderByDescending(p => p.CreatedDate).Select(p => new SummaryRequestModel
                {
                    Id = p.Id,
                    WareHouse = p.WareHouse.Address,
                    ExpectedDate = p.ExpectedDate,
                    Address = p.Address,
                    Status = p.Status,
                    Code = p.Code,
                    PickingDate = p.PickingDate,
                }).ToDataSourceResult(request);
            }
        }

        // For Customer to get request detail
        public async Task<CustomerRequestModel> FindCustomerRequestAsync(int requestId, long userId, string role)
        {
            if (role == "Customer")
            {

                return await this.dbSet
                                     .Include(p => p.WareHouse)
                                     .Where(p => p.Id == requestId && p.CustomerId == userId)
                                      .Select(p => new CustomerRequestModel
                                      {
                                          WareHouse = new DataSourceValue<int>()
                                          {
                                              Value = p.WareHouseId,
                                              DisplayName = p.WareHouse.Address
                                          },
                                          Id = p.Id,
                                          Status = p.Status,
                                          ExpectedDate = p.ExpectedDate,
                                          Address = p.Address,
                                          DeliveryLatitude = p.DeliveryLatitude,
                                          DeliveryLongitude = p.DeliveryLongitude,
                                          Code = p.Code,
                                          PackageQuantity = p.PackageQuantity,
                                          ReceiverName = p.ReceiverName,
                                          ReceiverPhoneNumber = p.ReceiverPhoneNumber,
                                          PickingDate = p.PickingDate,
                                      }).SingleOrDefaultAsync();
            }
            else
            {

                return await this.dbSet
                                     .Include(p => p.WareHouse)
                                     .Where(p => p.Id == requestId)
                                      .Select(p => new CustomerRequestModel
                                      {
                                          WareHouse = new DataSourceValue<int>()
                                          {
                                              Value = p.WareHouseId,
                                              DisplayName = p.WareHouse.Address
                                          },
                                          Id = p.Id,
                                          Status = p.Status,
                                          ExpectedDate = p.ExpectedDate,
                                          Address = p.Address,
                                          DeliveryLatitude = p.DeliveryLatitude,
                                          DeliveryLongitude = p.DeliveryLongitude,
                                          Code = p.Code,
                                          PackageQuantity = p.PackageQuantity,
                                          ReceiverName = p.ReceiverName,
                                          ReceiverPhoneNumber = p.ReceiverPhoneNumber,
                                          PickingDate = p.PickingDate,
                                      }).SingleOrDefaultAsync();
            }
        }

        // For Customer to change request status (Active/Deactive)
        public async Task<string> ChangeStatusAsync(int requestId, string status)
        {
            var entity = await this.dbSet.Where(p => p.Id == requestId).FirstAsync();
            entity.Status = status;
            this.context.Update(entity);
            await this.context.SaveChangesAsync();
            return entity.Status;
        }
        // End Đ
    }
}
