using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domains.Core;
using Domains.GoGo.Entities;
using Domains.GoGo.Models;
using Domains.GoGo.Models.Transportation;
using Domains.GoGo.Repositories;
using Domains.GoGo.Repositories.Transportation;
using Domains.Helpers;
using Groove.AspNetCore.Common.Identity;
using Groove.AspNetCore.UnitOfWork;
using Kendo.Mvc.UI;

namespace Domains.GoGo.Services.Transportation
{
	public class ShipmentService : IShipmentService
	{
		
		private readonly IShipmentRepository _shipmentRepository;
		private readonly IShipmentRequestRepository _shipmentRequestRepository;
		private readonly IWarehouseRepository _warehouseRepository;
		private readonly IRequestRepository _requestRepository;
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;

		public ShipmentService(IMapper mapper, IUnitOfWork uow, 
			IShipmentRepository repository, IRequestRepository requestRepository, 
			IShipmentRequestRepository shipmentRequestRepository, IWarehouseRepository warehouseRepository)
		{
			_uow = uow;
			_shipmentRepository = repository;
			_mapper = mapper;
			_requestRepository = requestRepository;
			_shipmentRequestRepository = shipmentRequestRepository;
			_warehouseRepository = warehouseRepository;
		}

        public async Task<int> ChangeShipmentStatusById(string id, string status)
        {
            return await _shipmentRepository.ChangeShipmentStatusById(id, status);
        }

		public async Task<string> ChangeDeliveryStatus(string code, string status)
		{
			return await _shipmentRepository.ChangeDeliveryStatus(code, status);
		}

		public async Task<int> CreateShipmentAsync(FormShipmentModel model)
		{
			var entity = _mapper.Map<Shipment>(model);

			//get Created Date
			entity.CreatedDate = DateTime.Parse(String.Format("{0:G}", DateTime.Now)); // TODO: Use DateTime.Now only

			entity.Code = Helper.GenerateCode(DateTime.Now, entity.CoordinatorId);

			entity.Status = ShipmentStatus.INACTIVE; 

			_uow.GetRepository<IShipmentRepository>().Create(entity);

			await _uow.SaveChangesAsync();
			
			return entity.Id;
		}

		public async Task UpdateShipmentByIdAsync(string code, FormShipmentModel model)
		{

			var entity = _shipmentRepository.GetShipment(model.Id.ToString());

			 _mapper.Map<FormShipmentModel, Shipment>(model, entity);
         
		    _shipmentRequestRepository.UpdateShipmentRequest(model.RequestIdList, model.Id);
			_uow.GetRepository<IShipmentRepository>().Update(entity);
				
            await _uow.SaveChangesAsync();
        }


		public DataSourceResult GetAllAsync([DataSourceRequest]DataSourceRequest request, string userId)
		{
			return _shipmentRepository.GetAllAsync(request, userId);
		}

		public ShipmentDetailModel GetShipmentById(string shipmentId)
		{
			var result = _shipmentRepository.GetShipmentById(shipmentId);

			result.RequestList = _requestRepository.GetRequestsByShipmentId(result.Id);
			result.RequestIdList = _requestRepository.GetRequestIdList(result.Id);

			result.Warehouse = _warehouseRepository.GetWarehouseByIdlAsync(shipmentId);

			return result;
		}
      
        public Task<ShipmentViewModel> GetShipmentAsync(string code)
        {
            return _shipmentRepository.GetShipmentAsync(code);
        }
    }
}
