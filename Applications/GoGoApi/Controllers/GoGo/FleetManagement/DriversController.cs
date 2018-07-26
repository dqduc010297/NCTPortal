using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains.GoGo.Services.Fleet_management;
using Groove.AspNetCore.Mvc;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoGoApi.Controllers
{
    [Route("api/MasterData/Drivers")]
    [ApiController]
    public class DriversController : BaseController
    {
        private readonly IDriverService _service;

        public DriversController(IDriverService service)
        {
            _service = service;
        }

        [Route("")]
		[Authorize(Roles = "Coordinator")]
		[HttpGet]
        public async Task<IActionResult> GetDataSource([FromQuery]string driverName)
        {

            return Ok(await _service.GetDataSource(driverName));
        }

        [Route("{id}")]
		[Authorize(Roles = "Coordinator")]
		[HttpGet]
        public async Task<IActionResult> GetRequestDetailAsync(string id)
        {

            return Ok(await _service.GetDriverDetail(id));
        }
    }
}