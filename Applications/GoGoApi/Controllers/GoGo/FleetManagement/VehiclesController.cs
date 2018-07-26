using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains.GoGo.Services.Fleet_management;
using Groove.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoGoApi.Controllers
{
    [Route("api/Vehicles")]
    [ApiController]
    public class VehiclesController : BaseController
    {
        private readonly IVehicleService _service;

        public VehiclesController(IVehicleService service)
        {
            _service = service;
        }

        [Route("")]
		[Authorize(Roles = "Coordinator")]
		[HttpGet]
        public async Task<IActionResult> GetDataSource([FromQuery]string licensePlate)
        {

            return Ok(await _service.GetDataSource(licensePlate));
        }

        [Route("{id}")]
		[Authorize(Roles = "Coordinator")]
		[HttpGet]
        public async Task<IActionResult> GetRequestDetailAsync(int id)
        {

            return Ok(await _service.GetVehicleDetailAsync(id));
        }
    }
}