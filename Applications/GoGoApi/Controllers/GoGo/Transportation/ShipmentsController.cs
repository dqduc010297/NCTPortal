using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domains.Core;
using Domains.GoGo.Models;
using Domains.GoGo.Models.Transportation;
using Domains.GoGo.Services;
using Domains.GoGo.Services.Transportation;
using Groove.AspNetCore.Mvc;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace GoGoApi.Controllers.GoGo
{
    // TODO: Authorization by role
	// TODO: change `Shipments` to `Shipment` because Pluralizing is hard to manage and It's useless here
	[Route("api/Shipments")]
	[ApiController]
	public class ShipmentsController : BaseController
	{
		private readonly IRequestService _service;
		private readonly IShipmentService _Shipmentservice;
		private readonly IShipmentRequestService _shipmentRequestService;
        private readonly IProblemMessageService _problemMessageService;
        public ShipmentsController(IRequestService service, IShipmentService Shipmentservice, IShipmentRequestService shipmentRequestService,IProblemMessageService problemMessageService)
		{
			_service = service;
			_Shipmentservice = Shipmentservice;
			_shipmentRequestService = shipmentRequestService;
            _problemMessageService = problemMessageService;
		}
		// TODO: change route to POST ""
		// Done
		[Route("")]
		[Authorize(Roles = "Coordinator")]
		[HttpPost]
		public async Task<IActionResult> CreateShipment(FormShipmentModel model)
		{
			var userIdentity = GetCurrentIdentity<long>();
			model.CoordinatorId = userIdentity.Id;
			var shipmentId = await _Shipmentservice.CreateShipmentAsync(model);
			await _shipmentRequestService.CreateShipmentRequestAsync(model.RequestIdList, shipmentId);

            return Ok();
        }

		// TODO: replace route by PUT "{code}/activate"
		// TODO: replace route by PUT "{code}/deactivate"
		// Done
		[Route("{id}/activate")]
		[Authorize(Roles = "Coordinator")]
		[HttpPut]
		public async Task<IActionResult> ActivateShipment(string id)
		{
			return Ok(await _Shipmentservice.ChangeShipmentStatusById(id, ShipmentStatus.WAITING));
		}

		[Route("{id}/deactivate")]
		[Authorize(Roles = "Coordinator")]
		[HttpPut]
		public async Task<IActionResult> DeactivateShipment(string id)
		{
			return Ok(await _Shipmentservice.ChangeShipmentStatusById(id, ShipmentStatus.INACTIVE));
		}

		[Route("")]
		[Authorize(Roles = "Driver,Coordinator")]
		[HttpGet]
		public IActionResult GetShipments([DataSourceRequest]DataSourceRequest queryString)
		{
			var userIdentity = GetCurrentIdentity<long>();
			var roles = this.User.Claims.Where(p => p.Type == ClaimTypes.Role).Select(p=>p.Value).ToList();


			if (roles.Any(p=> p == ApplicationRoles.COORDINATOR))
			{
				return Ok(_Shipmentservice.GetAllAsync(queryString, null));
			}
			else
			{
				return Ok(_Shipmentservice.GetAllAsync(queryString, userIdentity.Id.ToString()));
			}

		}

		[Route("{id}")]
		[Authorize(Roles = "Coordinator")]
		[HttpGet]
		public IActionResult GetShipmentDetail(string id)
		{
			return Ok(_Shipmentservice.GetShipmentById(id));
		}

		// TODO: change route to PUT "{code}" 
		// Done
		[Route("{id}")]
		[Authorize(Roles = "Coordinator")]
		[HttpPut]
		public async Task<IActionResult> UpdateShipment(string id, FormShipmentModel model)
		{
			await _Shipmentservice.UpdateShipmentByIdAsync(id, model);

			return Ok();
		}

        //DUC
        [Route("{shipmentCode}/deliverydetail")]
        [Authorize(Roles = "Driver,Coordinator")]
        [HttpGet]
        public async Task<IActionResult> GetShipmentDetailAsync(string shipmentCode)
        {
            var t = await _Shipmentservice.GetShipmentAsync(shipmentCode);
            return Ok(t);
        }


        [Route("{code}/request/{requestCode}/changestatus")]
        [Authorize(Roles = "Driver,Coordinator")]
        [HttpPut]
        public async Task<IActionResult> ChangeDeliveryStatus([FromBody]string requestCode, string status)
        {
            string code = await _shipmentRequestService.ChangeStatusRequestAsync(requestCode, status);
            return Ok(await _shipmentRequestService.GetCurrentRequestAsync(code));
        }

        [Route("{shipmentCode}/locationpicking")]
        [Authorize(Roles = "Driver,Coordinator")]
        [HttpGet]
        public async Task<IActionResult> GetLocationPicking(string shipmentCode)
        {
            return Ok(await _shipmentRequestService.GetPositionPicking(shipmentCode));
        }

        [Route("request/{requestCode}")]
        [Authorize(Roles = "Driver,Coordinator")]
        [HttpGet]
        public async Task<IActionResult> GetRequestDetailAsync(string requestCode)
        {
            return Ok(await _shipmentRequestService.GetCurrentRequestAsync(requestCode));
        }

        [Route("{shipmentCode}/requestList")]
        [Authorize(Roles = "Driver,Coordinator")]
        [HttpGet]
        public async Task<IActionResult> GetRequestList(string shipmentCode)
        {
            return Ok(await _shipmentRequestService.GetRequestListAsync(shipmentCode));
        }

        [Route("{shipmentCode}/changestatus/{status}")]
        [Authorize(Roles = "Driver")]
        [HttpPut]
        public async Task<IActionResult> ShipmentFeedback(string shipmentCode, string status)
        {
            string code = await _Shipmentservice.ChangeDeliveryStatus(shipmentCode, status);
            return Ok(await _Shipmentservice.GetShipmentAsync(code));
        }
        [Route("request/{requestCode}/changestatus/{status}")]
        [Authorize(Roles = "Driver")]
        [HttpPut]
        public async Task<IActionResult> ChangeStatusRequest(string requestCode, string status)
        {
            string code = await _shipmentRequestService.ChangeStatusRequestAsync(requestCode, status);
            return Ok(await _shipmentRequestService.GetCurrentRequestAsync(code));
        }
        [Route("request/{requestCode}/sendproblem")]
        [Authorize(Roles = "Driver")]
        [HttpPost]
        public async Task<IActionResult> SaveProblem(string requestCode, [FromBody]Message message)
        {
            int result = await _problemMessageService.SaveProblemMessageAsync(requestCode, message.message);
            string code = await _shipmentRequestService.Problem(requestCode, true);
            return Ok(await _shipmentRequestService.GetCurrentRequestAsync(code));
        }
        [Route("request/{requestCode}/resolveproblem")]
        [Authorize(Roles = "Driver")]
        [HttpPut]
        public async Task<IActionResult> ResolveProblem (string requestCode)
        {
            string code = await _shipmentRequestService.Problem(requestCode, false);
            return Ok(await _shipmentRequestService.GetCurrentRequestAsync(code));
        }
    }
    public class Message
    {
        public string message { set; get; }
    }
}
