using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domains.Core;
using Domains.GoGo.Models.Transportation;
using Domains.GoGo.Services;
using Domains.GoGo.Services.Transportation;
using Groove.AspNetCore.Mvc;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoGoApi.Controllers.GoGo
{
    // TODO: change Requests to `Request` because Pluralizing is hard to manage and It's useless here
    [Route("api/Requests")]
    [ApiController]
    public class RequestsController : BaseController
	{
		private readonly IRequestService _requestService;
		private readonly IShipmentService _Shipmentservice;
		private readonly IShipmentRequestService _shipmentRequestService;
        private readonly IVehicleFeatureRequestService _vehicleFeatureRequestService;

        public RequestsController(IRequestService requestService, IShipmentService Shipmentservice, IShipmentRequestService shipmentRequestService, IVehicleFeatureRequestService vehicleFeatureRequestService)
		{
			_requestService = requestService;
			_Shipmentservice = Shipmentservice;
			_shipmentRequestService = shipmentRequestService;
            _vehicleFeatureRequestService = vehicleFeatureRequestService;
		}

		[Route("filter/{warehouseId}/{value}")]
		[Authorize(Roles = "Coordinator")]
		[HttpGet]
		public async Task<IActionResult> GetDataSource(int warehouseId, string value)
		{
	
			return Ok(await _requestService.GetDataSource(value, warehouseId));
		}

        [Route("{id}/datasourcedetail")]
        [HttpGet]
        public async Task<IActionResult> GetRequestDataSourceDetailByIdAsync(string id)
        {

            return Ok(await _requestService.GetRequestByIdAsync(id));
        }

        [Route("filer/warehouse/{warehouseId}")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsyncByWarehouseId(string warehouseId)
        {

            return Ok(await _requestService.GetAllAsyncByWarehouseId(warehouseId));
        }


        // Đ
        // POST /api/requests
        [Route("")]
        [HttpPost]
		[Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateRequest([FromBody]CustomerRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = GetCurrentUserId<long>();
            var requestResult = await this._requestService.CreateCustomerRequest(model, userId);
            var saveToFeature = await this._vehicleFeatureRequestService.CreateVehicleFeatureRequest(requestResult, model.VehicleFeature.Value);
            return OkValueObject(requestResult);
        }

        // PUT /api/requests/{requestId}
        [Route("{requestId}")]
        [HttpPut]
		[Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateRequest([FromBody]CustomerRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (model.Status == RequestStatus.REJECTED)
            {
                model.Status = RequestStatus.INACTIVE;
            }

            var userId = GetCurrentUserId<long>();
            var requestResult = await this._requestService.UpdateCustomerRequest(model, userId);
            var featureResult = await this._vehicleFeatureRequestService.UpdateVehicleFeatureAsync(requestResult, model.VehicleFeature.Value);
            return OkValueObject(requestResult);
        }

        // GET /api/requests
        [Route("")]
        [HttpGet]
        [Authorize(Roles = "Customer, Coordinator")]
        public IActionResult GetRequests([DataSourceRequest]DataSourceRequest request)
        {
            var userId = GetCurrentUserId<long>();
            string role = this.User.Claims.Where(p => p.Type == ClaimTypes.Role).FirstOrDefault().Value;
            var result = _requestService.GetCustomerRequests(request, userId, role);
            return Ok(result);
        }

        // GET /api/requests/{requestId}
        [Route("{requestId}")]
        [HttpGet]
        [Authorize(Roles = "Customer, Coordinator")]
        public async Task<IActionResult> GetRequestAsync(int requestId)
        {
            var userId = GetCurrentUserId<long>();
            string role = this.User.Claims.Where(p => p.Type == ClaimTypes.Role).FirstOrDefault().Value;
            var requestResult = await _requestService.FindCustomerRequestAsync(requestId, userId, role);
            if (requestResult != null)
            {
                var featureResult = _vehicleFeatureRequestService.FindVehicleFeature(requestId);
                requestResult.VehicleFeature = featureResult;

                return Ok(requestResult);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT /api/requests/{requestId}/status
        [Route("{requestId}/status")]
        [HttpPut]
        [Authorize(Roles = "Customer, Coordinator")]
        public IActionResult ChangeStatus(int requestId,[FromBody] StringObject status)
        {
            if (status.content != RequestStatus.INACTIVE && status.content != RequestStatus.SENDING && status.content != RequestStatus.ACCEPTED && status.content != RequestStatus.REJECTED)
            {
                return BadRequest();
            }
            var result = _requestService.ChangeStatus(requestId, status.content);
            return Ok(result);
        }
		// End Đ

	}
}