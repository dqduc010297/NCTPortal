using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains.GoGo.Services.Transportation;
using Groove.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoGoApi.Controllers.GoGo
{
    [Route("api/ShipmentRequest")]
    public class ShipmentRequestController : BaseController
    {
        private readonly IShipmentRequestService _shipmentRequestService;

        public ShipmentRequestController(IShipmentRequestService shipmentRequestService)
        {
            _shipmentRequestService = shipmentRequestService;
        }

        // Đ

        // GET /api/shipmentrequest/{requestId}/status
        [Route("{requestId}/status")]
        [HttpGet]
        [Authorize(Roles = "Customer, Coordinator")]
        public IActionResult GetRequestStatus(int requestId)
        {
            var result = _shipmentRequestService.GetRequestStatus(requestId, 1);
            return Ok(new {result});
        }
        // End Đ
    }
}